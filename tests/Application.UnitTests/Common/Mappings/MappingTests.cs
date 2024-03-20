﻿using System.Reflection;
using System.Runtime.CompilerServices;
using AutoMapper;
using CleanArchitecture.Application.Common.Interfaces;
using NUnit.Framework;

namespace CleanArchitecture.Application.UnitTests.Common.Mappings;

public class MappingTests
{
    private readonly IConfigurationProvider _configuration;
    private readonly IMapper _mapper;

    public MappingTests()
    {
        _configuration = new MapperConfiguration(config =>
            config.AddMaps(Assembly.GetAssembly(typeof(IApplicationDbContext))));

        _mapper = _configuration.CreateMapper();
    }

    [Test]
    public void ShouldHaveValidConfiguration()
    {
        _configuration.AssertConfigurationIsValid();
    }

    // TODO: When implement Dtos this text will be needed
    //[Test]
    //[TestCase(typeof(TodoList), typeof(TodoListDto))]
    //[TestCase(typeof(TodoItem), typeof(TodoItemDto))]
    public void ShouldSupportMappingFromSourceToDestination(Type source, Type destination)
    {
        object instance = GetInstanceOf(source);

        _ = _mapper.Map(instance, source, destination);
    }

    private object GetInstanceOf(Type type)
    {
        if (type.GetConstructor(Type.EmptyTypes) != null)
        {
            return Activator.CreateInstance(type)!;
        }

        // Type without parameterless constructor
        return RuntimeHelpers.GetUninitializedObject(type);
    }
}
