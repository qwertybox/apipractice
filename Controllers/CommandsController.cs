using System.Collections.Generic;
using AutoMapper;
using Commander.Data;
using Commander.Dtos;
using Commander.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Commander.Controllers{
    [Route("api/commands")]
    [ApiController]
    public class CommandsController : ControllerBase{
        private readonly ICommanderRepo _repository;
        private readonly IMapper _mapper;

        public CommandsController(ICommanderRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        //Get api/commands
        [HttpGet]
        public ActionResult <IEnumerable<Command>> GetAllCommands(){
            var commandItems = _repository.GetAllCommands();
            return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(commandItems));
        }
        //Get api/commands/{id}
        [HttpGet("{id}", Name ="GetCommandById")]
        public ActionResult <CommandReadDto> GetCommandById(int id){
            var commandItem = _repository.GetCommandById(id);
            if (commandItem != null){
                return Ok(_mapper.Map<CommandReadDto>(commandItem));
            }
            return NotFound();
        }
        //post api /commands/{id}
        [HttpPost]
        public ActionResult <CommandReadDto> CreateCommand(CommandCreateDto commandCreateDto){
            var commandModel = _mapper.Map<Command>(commandCreateDto);
            _repository.CreateCommand(commandModel);
            _repository.SaveChanges();
            var commandReadDto = _mapper.Map<CommandReadDto>(commandModel);
            //return Ok(commandReadDto);
            return CreatedAtRoute(nameof(GetCommandById), new {Id = commandReadDto.Id}, commandReadDto);
        }
        //PUT api /commands/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateCommand(int id, CommandUpdateDto commandUpdateDto){
            var commandModelFromRepo = _repository.GetCommandById(id);
            if (commandModelFromRepo == null) {
                return NotFound();
            }
            _mapper.Map(commandUpdateDto, commandModelFromRepo);
            _repository.UpdateCommand(commandModelFromRepo);

            _repository.SaveChanges();


            return NoContent();
        }
        //PATCH api/commands/{id}
        [HttpPatch("{id")]
        public ActionResult PartialCommandUpdate(int id, JsonPatchDocument<CommandUpdateDto> patchDocument){
            //from request we have patch document, then we check we have all resources to update, than validete model and save changes
            var commandModelFromRepo = _repository.GetCommandById(id);
            if (commandModelFromRepo == null) {
                return NotFound();
            }
            var commandToPatch = _mapper.Map<CommandUpdateDto>(commandModelFromRepo);
            patchDocument.ApplyTo(commandToPatch, ModelState);
            if (!TryValidateModel(commandToPatch)){
                return ValidationProblem();
            }
            _mapper.Map(commandToPatch, commandModelFromRepo);
            _repository.UpdateCommand(commandModelFromRepo);
            _repository.SaveChanges();
            return NoContent();
        }
    }
}