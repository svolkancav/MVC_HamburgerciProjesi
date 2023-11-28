using Autofac;
using AutoMapper;
using HamburgerciProject.Application.AutoMapper;
using HamburgerciProject.Application.Models.DTOs;
using HamburgerciProject.Application.Services.AppUserService;
using HamburgerciProject.Application.Services.EkstraMalzemeServices;
using HamburgerciProject.Application.Services.MenuServices;
using HamburgerciProject.Application.Services.SepetServices;
using HamburgerciProject.Application.Services.SiparisServices;
using HamburgerciProject.Domain.Repositories;
using HamburgerciProject.Infrastructure.Repositories;
using X.PagedList;

namespace HamburgerciProject.Application.IoC
{
    public class DependencyResolver : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MenuService>().As<IMenuService>().InstancePerLifetimeScope();

            builder.RegisterType<MenuRepository>().As<IMenuRepository>().InstancePerLifetimeScope();

            builder.RegisterType<EkstraMalzemeService>().As<IEkstraMalzemeService>().InstancePerLifetimeScope();

            builder.RegisterType<EkstraMalzemeRepository>().As<IEkstraMalzemeRepository>().InstancePerLifetimeScope();

            builder.RegisterType<SiparisService>().As<ISiparisService>().InstancePerLifetimeScope();

            builder.RegisterType<SiparisRepository>().As<ISiparisRepository>().InstancePerLifetimeScope();

            builder.RegisterType<AppUserService>().As<IAppUserService>().InstancePerLifetimeScope();

            builder.RegisterType<AppUserRepository>().As<IAppUserRepository>().InstancePerLifetimeScope();

            builder.RegisterType<SepetRepository>().As<ISepetRepository>().InstancePerLifetimeScope();

            builder.RegisterType<SepetService>().As<ISepetService>().InstancePerLifetimeScope();

            builder.RegisterType<Mapper>().As<IMapper>().InstancePerLifetimeScope();


            #region AutoMapper
            builder.Register(context => new MapperConfiguration(cfg =>
            {
                

                //Register Mapper Profile
                cfg.AddProfile<Mapping>(); /// AutoMapper klasörünün altına eklediğimiz Mapping classını bağlıyoruz.
            }
            )).AsSelf().SingleInstance();

            builder.Register(c =>
            {
                //This resolves a new context that can be used later.
                var context = c.Resolve<IComponentContext>();
                var config = context.Resolve<MapperConfiguration>();
                return config.CreateMapper(context.Resolve);
            })
            .As<IMapper>()
            .InstancePerLifetimeScope();
            #endregion

            base.Load(builder);
        }
    }
}   
