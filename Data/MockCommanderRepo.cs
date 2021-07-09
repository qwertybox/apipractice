using System.Collections.Generic;
using Commander.Models;

namespace Commander.Data{
    public class MockCommanderRepo : ICommanderRepo
    {
        public void CreateCommand(Command cmd)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Command> GetAllCommands()
        {
            var commands = new List<Command>{
                new Command {Id=0, HowTo="Do smt", Line="Done smt", Platform="Pen"},
                new Command {Id=1, HowTo="Make a cup o tea", Line="Add sugar", Platform="sugar & tea"},
                new Command {Id=2, HowTo="Cut beard", Line="Get a knife", Platform="Beard & knife"}
            };
            return commands;
        }

        public Command GetCommandById(int id)
        {
            return new Command {Id=0, HowTo="Do smt", Line="Done smt", Platform="Pen"};
        }

        public bool SaveChanges()
        {
            throw new System.NotImplementedException();
        }

        public void UpdateCommand(Command cmd)
        {
            throw new System.NotImplementedException();
        }
    }
}