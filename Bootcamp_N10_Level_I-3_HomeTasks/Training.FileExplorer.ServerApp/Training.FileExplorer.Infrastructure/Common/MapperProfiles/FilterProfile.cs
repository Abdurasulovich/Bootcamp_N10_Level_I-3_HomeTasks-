﻿using AutoMapper;
using Training.FileExplorer.Application.FileStorage.Models.Storage;

namespace Training.FileExplorer.Infrastructure.Common.MapperProfiles;

public class FilterProfile : Profile
{
    public FilterProfile()
    {
        CreateMap<FileInfo, StorageFile>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Path, opt => opt.MapFrom(src => src.FullName))
            .ForMember(dest => dest.DirectoryPath, opt => opt.MapFrom(src => src.DirectoryName))
            .ForMember(dest => dest.Size, opt => opt.MapFrom(src => src.Length))
            .ForMember(dest => dest.Extention, opt => opt.MapFrom(src => src.Extension));
    }
}
