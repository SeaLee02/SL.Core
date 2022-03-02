# SL.Core
框架用于模块化开发,也适用于微服务开发模式。采用.net core 3.1 开发的webapi项目。

# 技术栈
- [x] 依赖注入(采用微软原生,看到时候需要改成AutoFac不)
- [x] AOP(Castle DynamicProxy)
- [x] AutoMapper进行实体映射
- [x] JWT自定义授权
- [x] 使用RBAC权限模式
- [x] 多租户模式
- [x] 组织数据权限
- [x] 使用 Swagger 做api文档
- [x] 采用 仓储+服务+接口的形式封装框架
- [x] 异步 async/await 开发
- [x] 封装SqlSugar数据库操作，支持分库，多库，读写分离,基础数据自动赋值,全局过滤等操作
- [x] 支持多种数据库，具体查看 SqlSugar
- [x] Serilog 日志
- [x] Apollo 配置
- [x] CSRedis操作Redis缓存
- [x] 中介者MediatR 处理进程内的事件
- [x] CAP,RabbitMQ 处理 进程之间的消息
- [x] 配套的代码生成工具，不写一行代码可以生成一个模块
- [] IdentityServer4 认证