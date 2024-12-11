using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Data;
using RestaurantAPI.DTOs;
using RestaurantAPI.Entities;
using RestaurantAPI.Enuns;
using RestaurantAPI.Interfaces;

namespace RestaurantAPI.Services
{
    public class ReserveService : IReserveService
    {
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;
        public ReserveService(ApplicationContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public string CreateReserve(ReserveDTO reserveDTO)
        {
            var table = _context.Tables.FirstOrDefault(t => t.Id == reserveDTO.TableId);

            if (table == null)
                throw new ArgumentException("Mesa não disponível ou inexistente.");

            if (table.Capacity < reserveDTO.PeopleNumber)
                throw new ArgumentException("Capacidade da mesa insuficiente.");

            var newReserve = new Reserve
            {
                TableId = reserveDTO.TableId,
                UserId = reserveDTO.UserId,
                PeopleNumber = reserveDTO.PeopleNumber,
                ReserveDate = DateTime.Now,
                Status = ReserveStatus.active
            };

            table.Status = TableStatus.reserved;

            _context.Reserves.Add(newReserve);
            _context.SaveChanges();

            return "Reserva criada com sucesso!";
        }

        public List<ReserveDetailsDTO> ListUserReserves(int userId)
        {
            var reserves = _context.Reserves
                .Where(r => r.UserId == userId)
                .Include(r => r.TableId)
                .ToList();

            return _mapper.Map<List<ReserveDetailsDTO>>(reserves);
        }

        public string CancelReserve(int reserveId)
        {
            var reserve = _context.Reserves.FirstOrDefault(r => r.Id == reserveId);

            if (reserve == null)
                throw new KeyNotFoundException("Reserva não encontrada ou já cancelada.");

            reserve.Status = ReserveStatus.canceled;

            var table = _context.Tables.FirstOrDefault(t => t.Id == reserve.TableId);

            if (table != null)
                table.Status = TableStatus.available;

            _context.SaveChanges();

            return "Reserva cancelada.";
        }
    }
}