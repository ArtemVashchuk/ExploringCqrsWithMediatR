﻿using Gatherly.Application.Members.Commands.CreateMember;
using Gatherly.Application.Members.Queries.GetMemberById;
using Gatherly.Domain.Shared;
using Gatherly.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Gatherly.Presentation.Controllers;

[Route("api/members")]
public sealed class MembersController(ISender sender) : ApiController(sender)
{
    [HttpPost]
    public async Task<IActionResult> RegisterMember([FromBody]RegisterMemberRequest request ,CancellationToken cancellationToken)
    {
        var command = new CreateMemberCommand(
            request.Email,
            request.FirstName,
            request.LastName);

        var result = await Sender.Send(command, cancellationToken);
        
        return result.IsSuccess
            ? Ok()
            : BadRequest(result.Error);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetMemberById(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetMemberByIdQuery(id);

        Result<MemberResponse> response = await Sender.Send(query, cancellationToken);

        return response.IsSuccess
            ? Ok(response.Value)
            : NotFound(response.Error);
    }
}