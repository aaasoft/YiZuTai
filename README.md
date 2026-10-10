# YiZuTai 易组态

基于 **Blazor** 的**可视化组态 / 低代码设计器组件库 + 运行时**，以 NuGet 分发。
提供「拖拽设计 → 属性编辑 → 序列化（XML）→ 运行时渲染 + 数据源 / 触发器驱动」的闭环能力。

> 适用场景：大屏可视化、物联网 / 工业组态、数据看板等需要「可视化编辑 + 运行时展示」的场景。

---

## 特性

- **可视化设计器**：拖拽布局、框选 / 多选、缩放、网格对齐、撤销 / 重做，配套 36 种字段类型属性编辑器（数字、枚举、开关、颜色、图片、集合……）。
- **运行时渲染**：`<View>` 组件按组态文件编号（`Id`）加载并渲染，无需重新编译即可更新界面。
- **控件体系**：基于 `BaseControl` + 反射 + 特性（`[Display]` / `[Category]` 等）驱动属性面板；自带 20+ 示例控件（文本、图表、进度、视频、网页、曲线、温湿度……）。
- **数据源**：内置定时刷新（`Timer`）、查询（`Query`）等抽象，支持外部值绑定与值变化联动刷新；`Project` 卸载时自动停止定时器并释放资源。
- **触发器**：按数据值条件执行动作（`BaseControlTrigger` 系列）。
- **超链接源**：点击控件跳转单 / 多链接（`IHyperLinkSource`）。
- **资源与文件**：组态文件 CRUD、导入 / 导出 `.yzt`、静态资源 HTTP 端点（含目录穿越防护）。
- **多框架**：支持 `net8.0` 与 `net10.0`。

---

## 架构概览

| 层 | 关键类型 | 职责 |
|---|---|---|
| 控件模型 | `BaseControl` → `BaseControl_WithBackground/AndAnimate`、`Project` | 布局 / 外观 / 层级，反射 + 特性驱动属性面板 |
| 设计器 UI | `Designer.razor(.cs)`、`PropertyGrid`、`View`、`CollectionEditControl` | 拖拽、多选、属性编辑、缩放、网格 |
| 属性编辑器 | `EditorManager` + 36 个 `Editor*` | 按字段类型映射编辑器（含 `short?/byte?/DateTime?` 等可空类型） |
| 数据源 | `AbstractDataSource` 系列、`IDataSource` / `IQueryDataSource` | 绑定外部值、定时 / 查询刷新 |
| 触发器 | `AbstractTrigger` + 4 类 | 按值条件执行动作 |
| 超链接源 | `IHyperLinkSource` | 点击跳转（单 / 多链接） |
| 运行时 / 持久化 | `ZTFileManager`、`SourceManager`、`YiZuTaiMiddlewareExtensions` | 组态文件 CRUD、导入导出、资源 / 下载 HTTP 端点 |
| 注册中心 | 单例 `ControlManager` / `EditorManager` / `SourceManager` | 反射注册控件 / 数据源 / 编辑器 |

**数据流**：设计器编辑 → `Project`（内存模型）↔ `ZTFileManager` 序列化（XML）→ 运行时 `View` 读取 `ZTFile` 渲染；数据源 / 触发器通过 `ObjectConnectionManager` 在值与控件之间建立联动。

---

## 安装

库工程 `YiZuTai` 在 CI（`publish-nuget.yml`，打 `v*` tag 时）自动打包为 NuGet 包。本地开发推荐直接引用工程：

```xml
<!-- 在你的 Blazor Server 工程中 -->
<ItemGroup>
  <ProjectReference Include="..\YiZuTai\YiZuTai.csproj" />
  <!-- 示例控件 / 数据源（可选，演示用） -->
  <ProjectReference Include="..\YiZuTai.Sample\YiZuTai.Sample.csproj" />
</ItemGroup>
```

---

## 快速开始

### 1. 注册控件 / 数据源

控件与具体数据源通过 `Provider.Register()` 反射注册（示例工程 `YiZuTai.Sample` 已实现，可直接复用或仿写）：

```csharp
YiZuTai.Sample.Provider.Register();
```

### 2. 注册服务（`Program.cs`）

```csharp
builder.Services.AddRazorComponents().AddInteractiveServerComponents();
builder.Services.AddControllers();
// 依赖的第三方服务
builder.Services.AddBlazorDownloadFile();
builder.Services.AddFileReaderService();
builder.Services.AddBlazorContextMenu();
```

### 3. 启用中间件

```csharp
var app = builder.Build();
// ……
app.MapRazorComponents<App>().AddInteractiveServerRenderMode();
app.UseYiZuTai();   // 挂载资源 / 导入 / 导出等 HTTP 端点
app.Run();
```

`UseYiZuTai` 可选传入 `YiZuTaiOptions`：

| 选项 | 默认值 | 说明 |
|---|---|---|
| `Prefix` | `YiZuTai` | 所有 HTTP 端点的 URL 前缀 |
| `DataFolder` | `Data` | 组态文件与资源存放目录 |

### 4. 渲染组态 / 打开设计器（Razor）

```razor
@page "/show/{id}"
<View Id="@Id" />

@page "/design/{id}"
<Designer Id="@Id" ShowToolBox="true" ShowPropertyBox="true" ShowDesignGrid="true" />
```

- `View`：运行时展示，`Id` 为组态文件编号（由 `ZTFileManager` 管理）。
- `Designer`：可视化编辑，`ShowToolBox` / `ShowPropertyBox` / `ShowDesignGrid` 控制面板显示，`DesignScale` 控制缩放。

> 设计器依赖 jQuery / layui 等静态资源，需随 `YiZuTai` 的 `wwwroot` 一并发布。

---

## 核心概念

- **组态文件（`ZTFile`）**：一个可视化界面的完整描述（XML 序列化），含 `Project`、资源目录、分组（`ZTGroup`）等。通过 `ZTFileManager.Instance` 进行 `Get` / `Save` / `Delete` / `GetGroups` / `Import` / `Export`。
- **控件（`BaseControl`）**：界面元素基类；派生类声明带 `[Display]` / `[Category]` 特性的属性，设计器据此自动生成属性面板。
- **数据源（`AbstractDataSource`）**：将外部值（时间、模拟数据、查询结果……）绑定到控件属性；支持定时自动刷新与查询刷新。`Project` 卸载时自动 `Stop` 并释放定时器。
- **触发器（`AbstractTrigger`）**：监听数据源 / 控件值，满足条件时执行动作。
- **超链接源（`IHyperLinkSource`）**：定义控件点击后的跳转行为（单链接 / 多链接）。

---

## 目录结构

```
YiZuTai/                # 核心库（NuGet 包）
  Components/           # Designer / View / PropertyGrid 等 UI 与 36 个属性编辑器
  Control/              # BaseControl 及派生控件基类
  Core/                 # ControlManager / EditorManager / SourceManager / ZTFileManager / ObjectConnection
  DataSource/           # 数据源抽象（定时 / 查询 / 自动刷新）与处理器
  Trigger/              # 触发器抽象
  HyperLinkSource/      # 超链接源抽象
  Model/                # ZTFile / ZTGroup 等模型
YiZuTai.Sample/         # 示例控件 + 数据源 + 注册入口（Provider.Register）
YiZuTaiTest/            # 运行型示例（YiQiDong 平台以 .ymg 镜像部署）
```

---

## 构建与开发

```bash
# 构建核心库（同时验证 net8.0 / net10.0）
dotnet build YiZuTai/YiZuTai.csproj

# 运行示例（含设计时与运行时）
dotnet run --project YiZuTaiTest
```
