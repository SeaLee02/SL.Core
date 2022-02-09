namespace SL.Module.Abstractions
{
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    /// <summary>
    /// 模块服务配置器接口
    /// <para>如果模块中有自己独有的服务需要注入，可以通过实现该接口来注入</para>
    /// Microsoft.Extensions.Hosting.Abstractions  nuget包
    /// </summary>
    public interface IModuleServicesConfigurator
    {
        /// <summary>
        /// 注入服务
        /// </summary>
        /// <param name="moduleConfigureContext"></param>
        void Configure(ModuleConfigureContext moduleConfigureContext);
    }
}
