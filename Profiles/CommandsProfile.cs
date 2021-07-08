//for linking dto with command object
using AutoMapper;
using Commander.Dtos;
using Commander.Models;

namespace CommanderProfiles{
    public class CommandsProfile : Profile{
        public CommandsProfile(){
            CreateMap<Command, CommandReadDto>();
            CreateMap<CommandCreateDto, Command>();
        }
    }
}