using AutoMapper;
using UmetnickaDela.Contracts.Models.Comments.Request;
using UmetnickaDela.Contracts.Models.Identity.Request;
using UmetnickaDela.Contracts.Models.Identity.Response;
using UmetnickaDela.Contracts.Models.Korpa.Request;
using UmetnickaDela.Contracts.Models.Masterpiece.Request;
using UmetnickaDela.Contracts.Models.Masterpiece.Response;
using UmetnickaDela.Contracts.Models.Rasprodaja.Requests;
using UmetnickaDela.Data.Models;

namespace NBP_projekat.Mapping
{
    public class ApplicationMapping : Profile
    {
        public ApplicationMapping()
        {
            CreateMap<RegisterRequest, User>();
            CreateMap<User, UserResponse>();
            CreateMap<UpdateUserRequest, User>();
            CreateMap<CreateMasterpieceRequesr, UmetnickoDelo>();
            CreateMap<UmetnickoDelo, CreateMasterpieceResponse>();
            CreateMap<CreateMasterpieceResponse, UmetnickoDelo>();
            CreateMap<UpdateMasterpieceRequest, UmetnickoDelo>();
           CreateMap<AddMarkRequest, UserDelo>();  
            CreateMap<UmetnickoDelo, GetMasterpieceResponse>();
            CreateMap<UmetnickoDelo, RasprodajaRequestCreateDTO>();
            CreateMap<RasprodajaRequestCreateDTO, UmetnickoDelo>();
            CreateMap<Korpa, AddKorpaRequest>();
            CreateMap<AddKorpaRequest, Korpa>();
            CreateMap<AddCommentRequest, Comments>();
            CreateMap<Comments, AddCommentRequest>();
        }
    }
}
