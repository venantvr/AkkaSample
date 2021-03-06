﻿using System;
using Akka.Actor;
using Akka.DI.AutoFac;
using AkkaSample.Actors.Handlers;
using AkkaSample.Actors.Managers;
using AkkaSample.Actors.SQL;
using AkkaSample.Contracts;
using AkkaSample.Factories;
using AkkaSample.Repositories;
using AkkaSample.Services;
using Autofac;

namespace AkkaSample
{
	public class Bootstrapper : IDisposable
	{
		public static IContainer Container { get; private set; }
		public static ActorSystem ActorSystem { get; private set; }
		public AutoFacDependencyResolver Resolver { get; private set; }

		public Bootstrapper()
		{
			ActorSystem = ActorSystem.Create("AkkaSample");
			var builder = new ContainerBuilder();

			ConfigureActors(builder);

			Container = builder.Build();

			Resolver = new AutoFacDependencyResolver(Container, ActorSystem);
		}

		private static void ConfigureActors(ContainerBuilder builder)
		{
			builder.RegisterType<ActorsFactory>().As<IActorsFactory>().WithParameter("system", ActorSystem).InstancePerDependency();
			builder.RegisterType<Repository>().As<IRepository>().InstancePerDependency();
			builder.RegisterType<EventSotre>().As<IEventStore>().InstancePerDependency();
			builder.RegisterType<TradingSystemService>().As<ITradingSystemService>().InstancePerDependency();
			builder.RegisterType<TradeManagerActor>().AsSelf().InstancePerDependency();
			builder.RegisterType<BuildSqlQueryActor>().AsSelf().InstancePerDependency();
			builder.RegisterType<ExecuteSqlQueryActor>().AsSelf().InstancePerDependency();
			builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies()).AsClosedTypesOf(typeof(IHandle<>)).InstancePerDependency();
		}

		public void Dispose()
		{
			ActorSystem.Terminate();
			ActorSystem.Dispose();
			Container.Dispose();
		}
	}
}
