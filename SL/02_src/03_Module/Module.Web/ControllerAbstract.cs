using SL.Module.Core;
using SL.Utils.Const;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SL.Module.Web
{
    /// <summary>
    /// 控制器抽象
    /// </summary>
    [Route("api/[area]/[controller]/[action]")]
    [ApiController]
    //自定义复杂的策略授权
    [Authorize(PolicyConst.TOKEN_POLICY)]
    //数据验证
    [ValidateResultFormat]
    public abstract class ControllerAbstract : ControllerBase
    {

    }
}
