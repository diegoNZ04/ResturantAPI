using RestaurantAPI.Data;
using RestaurantAPI.DTOs;
using AutoMapper;
using RestaurantAPI.Entities;
using RestaurantAPI.Services.Interfaces;

namespace RestaurantAPI.Services
{
    public class TableService : ITableService
    {
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;
        public TableService(ApplicationContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public string CreateTable(TableDTO tableDTO)
        {
            if (_context.Tables.Any(t => t.Name == tableDTO.Name))
            {
                throw new ArgumentException("Mesa já registrada.");
            }

            var table = _mapper.Map<Table>(tableDTO);

            _context.Tables.Add(table);
            _context.SaveChanges();

            return "Mesa registrada com sucesso!";
        }

        public void DeleteTableById(int tableId)
        {
            var table = GetTableById(tableId);

            if (table == null)
                throw new ArgumentException("Mesa inexistente.");

            _context.Tables.Remove(table);
            _context.SaveChanges();
        }

        public List<TableDTO> GetAllTables()
        {
            var tableDTOs = _mapper.Map<List<TableDTO>>(_context.Tables.ToList());

            return tableDTOs;
        }

        public void UpdateTable(int tableId, TableDTO tableDTO)
        {
            var table = GetTableById(tableId);

            if (table == null)
                throw new ArgumentException("Mesa inexistente.");

            table.Name = tableDTO.Name;
            table.Capacity = tableDTO.Capacity;
            table.Status = tableDTO.Status;

            _context.SaveChanges();
        }

        private Table GetTableById(int tableId)
        {
            var table = _context
                .Tables
                .FirstOrDefault(t => t.Id == tableId);

            return table;
        }
    }
}