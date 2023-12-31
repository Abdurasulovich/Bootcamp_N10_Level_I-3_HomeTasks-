﻿using AutoMapper;
using Training.FileExplorer.Api.Models.Dtos;
using Training.FileExplorer.Application.FileStorage.Models.Storage;

namespace Training.FileExplorer.Api.Common.MapperPrifiles;

public class DriveProfile : Profile
{
    public DriveProfile()
    {
        CreateMap<StorageDriveDto, StorageDrive>();
        CreateMap<StorageDrive, StorageDriveDto>();
    }
}
