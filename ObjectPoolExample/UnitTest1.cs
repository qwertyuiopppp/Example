using Microsoft.Extensions.ObjectPool;
using System;
using Xunit;

namespace ObjectPoolExample
{
	public class Demo
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public DateTime CreateTimte { get; set; }
	}

	public class DemoPooledObjectPolicy : IPooledObjectPolicy<Demo>
	{
		public Demo Create()
		{
			return new Demo { Id = 1, Name = "catcher", CreateTimte = DateTime.Now };
		}

		public bool Return(Demo obj)
		{
			return true;
		}
	}

	public class UnitTest1
	{
		[Fact(DisplayName = "从池中获取相同的对象")]
		public void Test1()
		{
			var demoPolicy = new DemoPooledObjectPolicy();
			var defaultPoolWithDemoPolicy = new DefaultObjectPool<Demo>(demoPolicy, 1);
			//获取一个对象
			var item1 = defaultPoolWithDemoPolicy.Get();
			//将对象扔回池中
			defaultPoolWithDemoPolicy.Return(item1);
			//获取一个对象
			var item2 = defaultPoolWithDemoPolicy.Get();
			Assert.True(item1 == item2);
		}

		[Fact(DisplayName = "从池中获取不同的对象")]
		public void Test2()
		{
			var demoPolicy = new DemoPooledObjectPolicy();
			var defaultPoolWithDemoPolicy = new DefaultObjectPool<Demo>(demoPolicy, 1);
			//获取一个对象
			var item1 = defaultPoolWithDemoPolicy.Get();
			//获取一个对象
			var item2 = defaultPoolWithDemoPolicy.Get();
			Assert.True(item1 != item2);
		}

		[Fact(DisplayName = "设置池大小")]
		public void Test3()
		{
			var demoPolicy = new DemoPooledObjectPolicy();
			var defaultPoolWithDemoPolicy = new DefaultObjectPool<Demo>(demoPolicy, 1);
			//获取一个对象
			var item1 = defaultPoolWithDemoPolicy.Get();
			//将对象扔回池中
			defaultPoolWithDemoPolicy.Return(item1);
			//获取一个对象
			var item2 = defaultPoolWithDemoPolicy.Get();
			//获取一个对象
			var item3 = defaultPoolWithDemoPolicy.Get();
			Assert.True((item1 == item2) && (item3 != item2));
		}
	}
}
