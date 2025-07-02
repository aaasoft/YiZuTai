using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace YiZuTai.Controllers
{
    [Route("File")]
    [ApiController()]
    public class ZTFileResourceController : ControllerBase
    {
        [HttpGet("{id}/" + Core.ZTFile.ZTFILE_RESOURCE_FOLDER + "/{*file}")]
        public ActionResult GetResource(string id, string file)
        {
            var ztFile = Core.ZTFileManager.Instance.Get(id);
            if (ztFile == null)
                return NotFound($"未找到编号为[{id}]的组态文件！");

            var fullFile = Path.Combine(ztFile.Folder, Core.ZTFile.ZTFILE_RESOURCE_FOLDER, file);
            if (!System.IO.File.Exists(fullFile))
                return NotFound($"在组态文件[{id}]目录未找到文件[{file}]！");

            var contentType = MimeMapping.MimeUtility.GetMimeMapping(fullFile);
            return PhysicalFile(fullFile, contentType);
        }
    }
}
