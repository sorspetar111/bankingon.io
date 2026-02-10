using System;
using System.Collections.Generic;
using System.Linq;

namespace ZooSimulation
{
    public abstract class Animal
    {
        protected const int MaxHealth = 100;
        protected const int MinHealth = 0;
        
        public string Name { get; }
        public int Health { get; protected set; }
        public abstract string Species { get; }
        public abstract bool IsDead { get; }
        
        protected Animal(string name, int initialHealth)
        {
            Name = name;
            Health = Math.Clamp(initialHealth, MinHealth, MaxHealth);
        }
        
        public virtual void ReduceHealth(int amount)
        {
            Health = Math.Max(MinHealth, Health - amount);
        }
        
        public virtual void IncreaseHealth(int amount)
        {
            Health = Math.Min(MaxHealth, Health + amount);
        }
        
        public virtual void Starve(int amount)
        {
            ReduceHealth(amount);
        }
        
        public virtual void Feed(int amount)
        {
            IncreaseHealth(amount);
        }
    }
    
    public class Monkey : Animal
    {
        public override string Species => "Monkey";
        public const int DeathThreshold = 40;
        
        public override bool IsDead => Health < DeathThreshold;
        
        public Monkey(string name, int initialHealth = MaxHealth) : base(name, initialHealth)
        {
        }
        
        public override string ToString()
        {
            return $"{Species} '{Name}' - Health: {Health}, Status: {(IsDead ? "Dead" : "Alive")}";
        }
    }
    
    public class Lion : Animal
    {
        public override string Species => "Lion";
        public const int DeathThreshold = 50;
        
        public override bool IsDead => Health < DeathThreshold;
        
        public Lion(string name, int initialHealth = MaxHealth) : base(name, initialHealth)
        {
        }
        
        public override string ToString()
        {
            return $"{Species} '{Name}' - Health: {Health}, Status: {(IsDead ? "Dead" : "Alive")}";
        }
    }
    
    public class Elephant : Animal
    {
        public override string Species => "Elephant";
        public const int WalkingThreshold = 70;
        public bool CanWalk => Health >= WalkingThreshold;

        public override bool IsDead => Health <= MinHealth;

        // public override bool IsDead => _isDead;
        // private bool _isDead;
        
        private bool _wasUnableToWalk;
        
        public Elephant(string name, int initialHealth = MaxHealth) : base(name, initialHealth)
        {
            UpdateWalkingStatus();
        }
        
        /*
        public override void ReduceHealth(int amount)
        {
            if (!CanWalk && _wasUnableToWalk)
            {
                _isDead = true;
                Health = MinHealth;
                return;
            }
            
            int previousHealth = Health;
            base.ReduceHealth(amount);
            
            UpdateWalkingStatus();
            
            if (!CanWalk && _wasUnableToWalk)
            {
                _isDead = true;
                Health = MinHealth;
            }
        }

        public override void ReduceHealth(int amount)
        {
            if (!CanWalk && _wasUnableToWalk)
            {
                IsDead = true;
                Health = MinHealth;
                return;
            }
            
            int previousHealth = Health;
            base.ReduceHealth(amount);
            
            UpdateWalkingStatus();
            
            if (!CanWalk && _wasUnableToWalk)
            {
                IsDead = true;
                Health = MinHealth;
            }
        }

        public override void ReduceHealth(int amount)
        {
            // Check if elephant is already dead
            if (_isDead) return;
            
            // If elephant can't walk BEFORE health reduction, it dies immediately
            // According to requirements: "If the elephant cannot walk when its health must be reduced, it dies"
            if (!CanWalk)
            {
                _isDead = true;
                Health = MinHealth;
                return;
            }
            
            // Only reduce health if elephant can walk
            base.ReduceHealth(amount);
            
            // After reduction, check if still alive (this is for display purposes)
            // The death condition is already handled above
        }

        public override void ReduceHealth(int amount)
        {
            if (_isDead) return;

            // Check if elephant can't walk BEFORE any reduction
            if (!CanWalk)  // Health < 70
            {
                _isDead = true;
                Health = MinHealth;
                return;  // Die immediately, no further reduction needed
            }
            
            // Only reduce health if elephant can walk
            base.ReduceHealth(amount);
            UpdateWalkingStatus();
        }
        */

        public override void ReduceHealth(int amount)
        {
            // If elephant cannot walk at the moment of reduction → dies
            if (!CanWalk)
            {
                Health = MinHealth;
                return;
            }

            base.ReduceHealth(amount);
            UpdateWalkingStatus();
        }
        
        public override void IncreaseHealth(int amount)
        {
            base.IncreaseHealth(amount);
            UpdateWalkingStatus();
        }
        
        private void UpdateWalkingStatus()
        {
            bool currentWalkingStatus = CanWalk;
            
            if (!currentWalkingStatus)
            {
                _wasUnableToWalk = true;
            }
            else
            {
                _wasUnableToWalk = false;
            }
        }
        
        public override string ToString()
        {
            string status = IsDead ? "Dead" : (CanWalk ? "Alive (Walking)" : "Alive (Can't Walk)");
            return $"{Species} '{Name}' - Health: {Health}, Status: {status}";
        }
    }
    
    public class Zoo
    {
        private readonly List<Animal> _animals = new List<Animal>();
        private readonly Random _random = new Random();
        
        public Zoo()
        {
            InitializeZoo();
        }
        
        private void InitializeZoo()
        {
            for (int i = 1; i <= 5; i++)
            {
                _animals.Add(new Monkey($"Monkey{i}"));
            }
            
            for (int i = 1; i <= 5; i++)
            {
                _animals.Add(new Lion($"Lion{i}"));
            }
            
            for (int i = 1; i <= 5; i++)
            {
                _animals.Add(new Elephant($"Elephant{i}"));
            }
        }
        
        public void SimulateStarvation()
        {
            Console.WriteLine("=== Simulating Starvation ===");
            
            foreach (var animal in _animals)
            {
                if (!animal.IsDead)
                {
                    int healthLoss = _random.Next(0, 21);
                    Console.WriteLine($"{animal.Name} loses {healthLoss} health");
                    animal.Starve(healthLoss);
                }
            }
        }
        
        public void SimulateFeeding()
        {
            Console.WriteLine("\n=== Simulating Feeding ===");
            
            int monkeyFood = _random.Next(5, 26);
            int lionFood = _random.Next(5, 26);
            int elephantFood = _random.Next(5, 26);
            
            Console.WriteLine($"Monkeys get +{monkeyFood} health");
            Console.WriteLine($"Lions get +{lionFood} health");
            Console.WriteLine($"Elephants get +{elephantFood} health");
            
            foreach (var animal in _animals)
            {
                if (!animal.IsDead)
                {
                    int foodAmount = animal switch
                    {
                        Monkey _ => monkeyFood,
                        Lion _ => lionFood,
                        Elephant _ => elephantFood,
                        _ => 0
                    };
                    
                    animal.Feed(foodAmount);
                }
            }
        }
        
        public int GetAliveAnimalCount()
        {
            return _animals.Count(animal => !animal.IsDead);
        }
        
        public Dictionary<string, int> GetAliveAnimalsBySpecies()
        {
            var result = new Dictionary<string, int>
            {
                { "Monkeys", _animals.OfType<Monkey>().Count(m => !m.IsDead) },
                { "Lions", _animals.OfType<Lion>().Count(l => !l.IsDead) },
                { "Elephants", _animals.OfType<Elephant>().Count(e => !e.IsDead) }
            };
            
            return result;
        }
        
        public void DisplayAllAnimals()
        {
            Console.WriteLine("\n=== All Animals in Zoo ===");
            Console.WriteLine($"Total animals: {_animals.Count}");
            Console.WriteLine($"Alive animals: {GetAliveAnimalCount()}");
            
            var bySpecies = GetAliveAnimalsBySpecies();
            Console.WriteLine($"\nAlive by species:");
            foreach (var species in bySpecies)
            {
                Console.WriteLine($"  {species.Key}: {species.Value}");
            }
            
            Console.WriteLine("\nDetailed status:");
            foreach (var animal in _animals)
            {
                Console.WriteLine($"  {animal}");
            }
        }
        
        public void DisplayAliveAnimals()
        {
            var aliveAnimals = _animals.Where(a => !a.IsDead).ToList();
            
            Console.WriteLine("\n=== Alive Animals ===");
            Console.WriteLine($"Count: {aliveAnimals.Count}");
            
            foreach (var animal in aliveAnimals)
            {
                Console.WriteLine($"  {animal}");
            }
        }
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            var zoo = new Zoo();
            
            Console.WriteLine("=== Zoo Simulation Started ===");
            Console.WriteLine($"Initial alive animals: {zoo.GetAliveAnimalCount()}");
            
            zoo.DisplayAllAnimals();
            
            for (int day = 1; day <= 5; day++)
            {
                Console.WriteLine($"\n\n=== Day {day} ===");
                
                zoo.SimulateStarvation();
                
                zoo.SimulateFeeding();
                
                Console.WriteLine($"\nAfter Day {day}:");
                Console.WriteLine($"Alive animals: {zoo.GetAliveAnimalCount()}");
                
                var aliveBySpecies = zoo.GetAliveAnimalsBySpecies();
                foreach (var species in aliveBySpecies)
                {
                    Console.WriteLine($"  {species.Key}: {species.Value} alive");
                }
                
                if (day % 2 == 0)
                {
                    zoo.DisplayAliveAnimals();
                }
            }
            
            Console.WriteLine("\n\n=== Final State ===");
            zoo.DisplayAllAnimals();
            
            Console.WriteLine("\n=== Simulation Statistics ===");
            Console.WriteLine($"Total days simulated: 5");
            Console.WriteLine($"Final alive count: {zoo.GetAliveAnimalCount()}");
            
            var finalSpeciesCount = zoo.GetAliveAnimalsBySpecies();
            foreach (var species in finalSpeciesCount)
            {
                Console.WriteLine($"  {species.Key}: {species.Value} survived");
            }
        }
    }
}
