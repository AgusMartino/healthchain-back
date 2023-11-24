// See https://aka.ms/new-console-template for more information
using Nethereum.Hex.HexTypes;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using SmartContract.Contracts.HealthChain;
using SmartContract.Contracts.HealthChain.ContractDefinition;
using System.Net;
using System.Numerics;
using Newtonsoft.Json.Linq;
using System.Text.Json.Nodes;
using Nethereum.Contracts.Standards.ERC1155.ContractDefinition;
using System.Security.Cryptography;
using Nethereum.Signer;
using Org.BouncyCastle.Utilities.Net;

try
{
    Console.WriteLine("Hello, World!");
    /*
    var privateKey = "";
    using (var rng = RandomNumberGenerator.Create())
    {
        var privateKeyBytes = new byte[32];
        rng.GetBytes(privateKeyBytes);

        privateKey = "0x" + BitConverter.ToString(privateKeyBytes).Replace("-", "").ToLower();
    }
    Console.WriteLine(privateKey);
    var _chainId = 5; //Nethereum test chain, chainId
    var account = new Account(privateKey, _chainId);
    Console.WriteLine(account.Address);*/


    var privateKey = "fdc83fd884ae517f4772c462f794f254f2c499430e265ca724310ecd99b39f62";
    var chainid = 5;
    var account = new Account(privateKey, chainid);
    var web3 = new Web3(account, "https://goerli.infura.io/v3/bdefe16b41da472991a8439fc5398b5d");
    var contractAddress = "0x2Ec565612a9dCfD95fB7984bf7f39F9eD3550455"; // Dirección del contrato ERC-721
    var tokenIdToMint = 123456; // ID del NFT que deseas crear
    var toAddress = "0x7d1D9Cd8Bc9420e0FD06067869874C53aa2a70ED"; // Dirección a la que se enviará el nuevo NFT

    var mintNFTABI = @"function mintNFT(address to, uint256 tokenId) public";

    var contract = web3.Eth.GetContract(mintNFTABI, contractAddress);

    var mintFunction = contract.GetFunction("mintNFT");

    var transactionHash = await mintFunction.SendTransactionAsync(account.Address, gas: new HexBigInteger(200000), value: new HexBigInteger(0),
                                               functionInput: new object[] { toAddress, tokenIdToMint });

    Console.WriteLine($"Transacción enviada. Hash: {transactionHash}");
}
catch (Exception ex)
{
    Console.WriteLine("Error: " + ex.Message);
}

