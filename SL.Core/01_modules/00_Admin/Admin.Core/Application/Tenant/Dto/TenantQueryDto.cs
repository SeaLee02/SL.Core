using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SL.Data.Abstractions.Query;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SL.Mkh.Admin.Core.Application.Tenant.Dto
{
    /// <summary>
    /// 查询
    /// </summary>
    public class TenantQueryDto: QueryDto
    {
        [JsonIgnore]
        public string Name  { get; set; }
    }
}
