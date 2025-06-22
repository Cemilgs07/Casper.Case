using AutoMapper;
using Case.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Case.API.Controllers
{
    public abstract class BaseController : ControllerBase
    {
        internal readonly IUnitOfWork _unitOfWork;
        internal readonly IMapper _mapper;
        internal readonly ILogger<BaseController> _logger;


        public BaseController(IUnitOfWork unitOfWork, IMapper mapper, ILogger<BaseController> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }
    }
}
