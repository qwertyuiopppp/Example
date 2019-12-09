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
		[Fact(DisplayName = "�ӳ��л�ȡ��ͬ�Ķ���")]
		public void Test1()
		{
			var demoPolicy = new DemoPooledObjectPolicy();
			var defaultPoolWithDemoPolicy = new DefaultObjectPool<Demo>(demoPolicy, 1);
			//��ȡһ������
			var item1 = defaultPoolWithDemoPolicy.Get();
			//�������ӻس���
			defaultPoolWithDemoPolicy.Return(item1);
			//��ȡһ������
			var item2 = defaultPoolWithDemoPolicy.Get();
			Assert.True(item1 == item2);
		}

		[Fact(DisplayName = "�ӳ��л�ȡ��ͬ�Ķ���")]
		public void Test2()
		{
			var demoPolicy = new DemoPooledObjectPolicy();
			var defaultPoolWithDemoPolicy = new DefaultObjectPool<Demo>(demoPolicy, 1);
			//��ȡһ������
			var item1 = defaultPoolWithDemoPolicy.Get();
			//��ȡһ������
			var item2 = defaultPoolWithDemoPolicy.Get();
			Assert.True(item1 != item2);
		}

		[Fact(DisplayName = "���óش�С")]
		public void Test3()
		{
			var demoPolicy = new DemoPooledObjectPolicy();
			var defaultPoolWithDemoPolicy = new DefaultObjectPool<Demo>(demoPolicy, 1);
			//��ȡһ������
			var item1 = defaultPoolWithDemoPolicy.Get();
			//�������ӻس���
			defaultPoolWithDemoPolicy.Return(item1);
			//��ȡһ������
			var item2 = defaultPoolWithDemoPolicy.Get();
			//��ȡһ������
			var item3 = defaultPoolWithDemoPolicy.Get();
			Assert.True((item1 == item2) && (item3 != item2));
		}
	}
}
