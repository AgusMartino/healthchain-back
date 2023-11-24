using BLL.Services;
using DOMAIN.DomainRequest;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIBlockChain.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class NFTController : Controller
    {
        [HttpPost(Name = "AddNFT")]
        public async Task<ActionResult> AddNFT(NftRequest nftRequest)
        {
            try
            {
                NFTService.Current.AddNFT(nftRequest);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
        }

        [HttpGet("{tokenid}", Name = "getNFT")]
        public async Task<ActionResult<NftRequest>> getNFT(string tokenid)
        {
            try
            {
                NftRequest nft = new NftRequest();
                nft = NFTService.Current.GetNFT(tokenid);
                return nft;
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
        }

        [HttpGet("{cuit}", Name = "GetNFTMarketplace")]
        public async Task<ActionResult<List<NftRequest>>> GetNFTMarketplace(string cuit)
        {
            try
            {
                List<NftRequest> nft = new List<NftRequest>();
                nft = (List<NftRequest>)NFTService.Current.GetNFTMarketplace(cuit);
                return nft;
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
        }

        [HttpGet("{id_user}",Name = "GetNFTCompany")]
        public async Task<ActionResult<List<NftRequest>>> GetNFTCompany(string id_user)
        {
            try
            {
                List<NftRequest> nft = new List<NftRequest>();
                nft = (List<NftRequest>)NFTService.Current.GetNFTCompany(id_user);
                return nft;
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
        }

        [HttpGet("{id_user}",Name = "GetNFTUser")]
        public async Task<ActionResult<List<NftRequest>>> GetNFTUser(string id_user)
        {
            try
            {
                List<NftRequest> nft = new List<NftRequest>();
                nft = (List<NftRequest>)NFTService.Current.GetNFTUser(id_user);
                return nft;
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
        }

        [HttpPost(Name = "SellNFT")]
        public async Task<ActionResult> SellNFT(NftRequest nftRequest)
        {
            try
            {
                NFTService.Current.SellNFT(nftRequest);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
        }

        [HttpPost(Name = "TransaferNFT")]
        public async Task<ActionResult> TransaferNFT(NftRequest nftRequest)
        {
            try
            {
                NFTService.Current.TransferNFT(nftRequest);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
        }

        [HttpPost(Name = "TransaferNFTWithETH")]
        public async Task<ActionResult> TransaferNFTWithETH(NftRequest nftRequest)
        {
            try
            {
                NFTService.Current.TranferNFTWithETH(nftRequest);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
        }
    }
}
