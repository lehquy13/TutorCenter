﻿namespace TutorCenter.Application.Contracts.Models;

public class PaginationParams
{
    public int PageIndex { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}