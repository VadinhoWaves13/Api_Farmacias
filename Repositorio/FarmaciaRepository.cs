using Api_Farmacias.Database;
using Api_Farmacias.Model;
using Api_Farmacias.Repositorio.InterFace;
using AutoMapper;
using Microsoft.EntityFrameworkCore;


namespace Api_Farmancias.Repositorio
{
    public class FarmaciaRepository : IFarmaciaRepisitory
    {
        private readonly Appdbcontext _conexao;
        private readonly IMapper _mapper;


        public FarmaciaRepository(Appdbcontext conexaoDB,IMapper mapper)
        {
            _conexao = conexaoDB;
            _mapper= mapper;

        }

       public async Task<List<FarmaciaDTO>> Farmancias()
        {
            var ListFArm = await _conexao.farmacias.ToListAsync();
            var farmDTO =_mapper.Map<List<FarmaciaDTO>>(ListFArm);
            return farmDTO;
        }
        public async Task<FarmaciaDTO> BuscarFarmacia(int id)
        {
            var farmacia = await _conexao.farmacias.Where(x => x.Id == id).FirstOrDefaultAsync();
            var farmDTO = _mapper.Map<FarmaciaDTO>(farmacia);
            return farmDTO ;
        }

        public async Task<Farmacia> AdicionarFarmacia(FarmaciaDTO farmacia)
        {
            var farmDTO =_mapper.Map<Farmacia>(farmacia);
            await _conexao.farmacias.AddAsync(farmDTO);
            await _conexao.SaveChangesAsync();
            return farmDTO;
        }

       public async Task<Farmacia> Atualizar(FarmaciaDTO farmacia, int id)
        {
            var farmaciaid = await BuscarFarmacia(id);
            if(farmaciaid == null)
            {
                throw new Exception($"Farmacia para o ID:{id} nao encontrado.");
            }

            var farmDTO = _mapper.Map<Farmacia>(farmaciaid);
            _conexao.farmacias.Update(farmDTO);
            await _conexao.SaveChangesAsync();

            return farmDTO;
        }

        public async Task<bool> Apagar(int id)
        {
            var farmaciaid = await BuscarFarmacia(id);
            if(farmaciaid == null)
            {
                throw new Exception($"Farmacia para o ID:{id} nao encontrado.");
            }
            var farmDTO =_mapper.Map<Farmacia>(farmaciaid);

            _conexao.farmacias.Remove(farmDTO);
            await _conexao.SaveChangesAsync();
            return true;
        }

    }
}