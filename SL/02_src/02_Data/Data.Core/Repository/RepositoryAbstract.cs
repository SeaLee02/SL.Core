using SL.Data.Abstractions;
using SL.Data.Abstractions.Entities;
using SL.Data.Abstractions.Login;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SL.Data.Core.Repository
{
    public abstract partial class RepositoryAbstract<TEntity> : IRepository<TEntity> where TEntity : class, new()
    {
        /// <summary>
        /// 工作单元
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;
        /// <summary>
        /// 事务数据操作
        /// </summary>
        private SqlSugarScope _dbBase;
        /// <summary>
        /// 登陆人信息
        /// </summary>
        private IUserResolver _userResolver;

        /// <summary>
        /// 单表操作，切库使用
        /// </summary>
        private ISqlSugarClient _db
        {
            get; set;
            //get
            //{
            //    var configId = typeof(TEntity).GetCustomAttribute<TenantAttribute>()?.configId ?? 0;
            //    _dbBase.ChangeDatabase(configId);
            //    return _dbBase;
            //}

        }

        /// <summary>
        /// 用于基类,实体仓储层中使用
        /// </summary>
        public ISqlSugarClient Db
        {
            get { return _db; }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="userResolver"></param>
        public RepositoryAbstract(IUnitOfWork unitOfWork, IUserResolver userResolver)
        {
            _unitOfWork = unitOfWork;
            _dbBase = unitOfWork.GetDbClient();
            _userResolver = userResolver;

            var configId = typeof(TEntity).GetCustomAttribute<TenantAttribute>()?.configId ?? 0;
            //单表操作
            _db = _dbBase.GetConnection(configId);
        }
       

     

        
    }
}
