using System;
using System.ComponentModel;
using YiZuTai.Source.DataSource;

namespace YiZuTai.Design;

public partial class BaseControlView : IDisposable
{
    private INotifyPropertyChanging notifyPropertyChangingModel;
    private INotifyPropertyChanged notifyPropertyChangedModel;
    private Dictionary<string, DataSourceInfo> dataSourceInfoDict;

    public class DataSourceInfo
    {
        public string Name { get; set; }
        public Func<IDataSource> GetSourceFunc { get; set; }
        public DataSourceInfo() { }
        public DataSourceInfo(string name, Func<IDataSource> getSourceFunc)
        {
            Name = name;
            GetSourceFunc = getSourceFunc;
        }
    }

    private void OnDataSource_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        InvokeAsync(StateHasChanged);
    }

    private void OnModel_PropertyChanging(object sender, PropertyChangingEventArgs e)
    {
        if (!dataSourceInfoDict.TryGetValue(e.PropertyName, out var dataSourceInfo))
            return;
        var dataSource = dataSourceInfo.GetSourceFunc.Invoke();
        if (dataSource != null)
            dataSource.PropertyChanged -= OnDataSource_PropertyChanged;
    }

    private void OnModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (!dataSourceInfoDict.TryGetValue(e.PropertyName, out var dataSourceInfo))
            return;
        var dataSource = dataSourceInfo.GetSourceFunc.Invoke();
        if (dataSource != null)
            dataSource.PropertyChanged += OnDataSource_PropertyChanged;
    }

    public void RegisterDataSource<T>(T model, params DataSourceInfo[] dataSourceInfos)
        where T : INotifyPropertyChanging, INotifyPropertyChanged
    {
        notifyPropertyChangingModel = model;
        notifyPropertyChangedModel = model;
        dataSourceInfoDict = dataSourceInfos.ToDictionary(t => t.Name, t => t);

        foreach (var dataSourceInfo in dataSourceInfos)
        {
            var dataSource = dataSourceInfo.GetSourceFunc.Invoke();
            if (dataSource != null)
                dataSource.PropertyChanged += OnDataSource_PropertyChanged;
        }
        model.PropertyChanging += OnModel_PropertyChanging;
        model.PropertyChanged += OnModel_PropertyChanged;
    }

    public void Dispose()
    {
        if (notifyPropertyChangingModel != null)
            notifyPropertyChangingModel.PropertyChanging -= OnModel_PropertyChanging;
        if (notifyPropertyChangedModel != null)
            notifyPropertyChangedModel.PropertyChanged -= OnModel_PropertyChanged;

        if (dataSourceInfoDict != null)
        {
            foreach (var dataSourceInfo in dataSourceInfoDict.Values)
            {
                var dataSource = dataSourceInfo.GetSourceFunc.Invoke();
                if (dataSource != null)
                    dataSource.PropertyChanged -= OnDataSource_PropertyChanged;
            }
        }
    }
}
