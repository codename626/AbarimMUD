﻿namespace AbarimMUD.Data
{
	public class Attack
	{
		private int _accuracy;

		public AttackType AttackType { get; set; }

		public int Accuracy
		{
			get => _accuracy;
			set
			{
				_accuracy = value;
			}
		}

		public RandomRange DamageRange;

		public Attack()
		{
		}

		public Attack(AttackType attackType, int accuracy, RandomRange damageRange)
		{
			AttackType = attackType;
			Accuracy = accuracy;
			DamageRange = damageRange;
		}

		public Attack(AttackType attackType, int accuracy, int min, int max) : this(attackType, accuracy, new RandomRange(min, max))
		{
		}

		public override string ToString()
		{
			return $"{AttackType}, {Accuracy}, {DamageRange}";
		}
	}
}
