﻿using Gatherly.Domain.Shared;
using MediatR;

namespace Gatherly.Application.Messaging;

public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>;