using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentMerchant.Core.map
{
    public class MapProfile: Profile
    {
        public MapProfile()
        {
            CreateMap<Core.Dtos.Transaction, Core.Entities.Transaction>();
            CreateMap<Core.Dtos.PaymentRequest, Core.Dtos.Transaction>();
            CreateMap<Core.Dtos.PaymentRequest, Core.Dtos.CreditCard>();
            CreateMap<Core.Dtos.CreditCard, Core.Entities.CreditCard>();
        }
    }
}
