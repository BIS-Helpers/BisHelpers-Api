﻿using BisHelpers.Application.Services.AcademicCourseService;
using BisHelpers.Application.Services.ProfessorService;

namespace BisHelpers.web.RouteGroups.Groups;

public static class AcademicCourseGroup
{
    public static RouteGroupBuilder GroupAcademicCourseVersionOne(this RouteGroupBuilder builder)
    {
        builder.MapPost("/ProfessorWithLectures", [Authorize(Roles = AppRoles.Admin)]
        async ([FromBody] CreateProfessorAcademicCourseDto dto, IValidator<CreateProfessorAcademicCourseDto> validator, IAcademicCourseService academicCourseService, HttpContext context) =>
        {
            var validationResult = validator.Validate(dto);

            if (!validationResult.IsValid)
                return Results.BadRequest(new ErrorDto(context)
                {
                    StatusCode = 400,
                    Errors = validationResult.ToErrorList(),
                });

            var addResponse = await academicCourseService.AddProfessorAsync(dto, context.User.GetUserId());

            if (!addResponse.IsSuccess)
                return Results.BadRequest(new ErrorDto(context)
                {
                    Errors = [addResponse.ErrorBody],
                    StatusCode = 400,
                });

            return Results.Created();
        })
        .EndPointConfigurations(Name: "Add Professor To Course", version: Versions.Version1)
        .CreatedResponseConfiguration()
        .ErrorResponseConfiguration(StatusCodes.Status400BadRequest)
        .UnauthorizedResponseConfiguration();

        builder.MapGet("/", [Authorize]
        async (IAcademicCourseService academicCourseService, HttpContext context) =>
        {
            var courses = await academicCourseService.GetAll();

            if (courses is null)
                return Results.NotFound();

            return Results.Ok(courses.MapToDto());
        })
        .EndPointConfigurations(Name: "Get All Academic Courses", version: Versions.Version1)
        .OkResponseConfiguration<IEnumerable<AcademicCourseDto>>()
        .ErrorResponseConfiguration(StatusCodes.Status404NotFound, withBody: false)
        .UnauthorizedResponseConfiguration();

        builder.MapGet("/{id}", [Authorize]
        async (int id, IAcademicCourseService academicCourseService, HttpContext context) =>
        {
            var course = await academicCourseService.GetById(id);

            if (course is null)
                return Results.NotFound();

            var dto = course.MapToDto(isDetailed: context.User.IsInRole(AppRoles.Admin));

            return Results.Ok(dto);
        })
        .EndPointConfigurations(Name: "Get Academic Course By Id", version: Versions.Version1)
        .OkResponseConfiguration<AcademicCourseDetailedDto>()
        .ErrorResponseConfiguration(StatusCodes.Status404NotFound, withBody: false)
        .UnauthorizedResponseConfiguration();

        builder.MapGet("/{academicCourseId}/Professors", [Authorize]
        async (int academicCourseId, IProfessorService professorService, HttpContext context) =>
        {
            var professors = await professorService.GetAllAsync(academicCourseId);

            if (professors is null)
                return Results.NotFound();

            return Results.Ok(professors);
        })
        .EndPointConfigurations(Name: "Get Professors By Academic Course Id", version: Versions.Version1)
        .OkResponseConfiguration<ProfessorWithLecturesDto>()
        .ErrorResponseConfiguration(StatusCodes.Status404NotFound, withBody: false)
        .UnauthorizedResponseConfiguration();

        return builder;
    }
}
