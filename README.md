# SL.Core
框架用于模块化开发,也适用于微服务开发模式。采用.net core 3.1 开发的webapi项目。

# 技术栈
- [√] 依赖注入(采用微软原生,看到时候需要改成AutoFac不)
- [√] AOP(Castle DynamicProxy)
- [√] AutoMapper进行实体映射
- [√] JWT自定义授权
- [√] 使用RBAC权限模式
- [√] 多租户模式
- [√] 组织数据权限
- [√] 使用 Swagger 做api文档
- [√] 采用 仓储+服务+接口的形式封装框架
- [√] 异步 async/await 开发
- [√] 封装SqlSugar数据库操作，支持分库，多库，读写分离,基础数据自动赋值,全局过滤等操作
- [√] 支持多种数据库，具体查看 SqlSugar
- [√] Serilog 日志
- [√] Apollo 配置
- [√] CSRedis操作Redis缓存
- [√] 中介者MediatR 处理进程内的事件
- [√] CAP,RabbitMQ 处理 进程之间的消息
- [√] 配套的代码生成工具，不写一行代码可以生成一个模块
- [x] IdentityServer4 认证