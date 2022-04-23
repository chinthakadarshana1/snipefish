﻿using AutoMapper;
using Snipefish.Application.Commands.Todo;
using Snipefish.Application.Responses;
using Snipefish.Domain.Entities;

namespace Snipefish.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Todo, TodoResponse>().ReverseMap();
            CreateMap<Todo, AddTodoCommand>().ReverseMap();
            CreateMap<Todo, UpdateTodoCommand>().ReverseMap();
        }
    }
}