using System;
using NUnit.Framework;
using Expedia;
using System.Collections.Generic;
using Rhino.Mocks;

namespace ExpediaTest
{
	[TestFixture()]
	public class CarTest
	{	
		private Car targetCar;
		private MockRepository mocks;
		
		[SetUp()]
		public void SetUp()
		{
			targetCar = new Car(5);
			mocks = new MockRepository();
		}
		
		[Test()]
		public void TestThatCarInitializes()
		{
			Assert.IsNotNull(targetCar);
		}	
		
		[Test()]
		public void TestThatCarHasCorrectBasePriceForFiveDays()
		{
			Assert.AreEqual(50, targetCar.getBasePrice()	);
		}
		
		[Test()]
		public void TestThatCarHasCorrectBasePriceForTenDays()
		{
            var target = new Car(10);
			Assert.AreEqual(80, target.getBasePrice());	
		}
		
		[Test()]
		public void TestThatCarHasCorrectBasePriceForSevenDays()
		{
			var target = new Car(7);
			Assert.AreEqual(10*7*.8, target.getBasePrice());
		}

        [Test()]
        public void TestThatCarHasCorrectBasePriceForTenDaysUsingObjectMotherBMW()
        {
            var target = ObjectMother.BMW();
            Assert.AreEqual(10 * 10 * .8, target.getBasePrice());
        }
		
		[Test()]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void TestThatCarThrowsOnBadLength()
		{
			new Car(-5);
		}

        [Test()]
        public void TestGetCarLocationFromDatabase()
        {
            IDatabase mockDatabase = mocks.Stub<IDatabase>();
            String carlocation = "Chicago";
            String anothercarlocation = "Terre Haute";
            using (mocks.Record())
            {
                mockDatabase.getCarLocation(22);
                LastCall.Return(carlocation);
                mockDatabase.getCarLocation(24);
                LastCall.Return(anothercarlocation);
            }
            var target = new Car(5);
            target.Database = mockDatabase;
            String result;
            result = target.getCarLocation(22);
            Assert.AreEqual(result, carlocation);
            result = target.getCarLocation(24);
            Assert.AreEqual(result, anothercarlocation);
        }

        [Test()]
        public void TestThatCarDoesGetMileageFromDatabase()
        {
            IDatabase mockDatabase = mocks.Stub<IDatabase>();
            Int32 Miles = 1000;
            mockDatabase.Miles = Miles;

            var target = new Car(5);
            target.Database = mockDatabase;

            int Mileage = target.Mileage;
            Assert.AreEqual(Mileage, Miles);
        }
        
	}
}
