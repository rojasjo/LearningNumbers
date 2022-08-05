using System;
using LearningNumbers.Services;

namespace LearningNumbers.Models
{
    public abstract class Calculation
    {
        public int First { get; set; }

        private int _second;

        public virtual int Second
        {
            get => _second;
            set => _second = value;
        }

        public abstract Operator Symbol { get; }
        
        public abstract int Calculate();

        public abstract void Accept(IVisitor visitor);
    }
}