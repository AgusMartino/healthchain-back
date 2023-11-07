// See https://aka.ms/new-console-template for more information
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using SmartContract.Contracts.NFTPatientHealthChain;
using SmartContract.Contracts.NFTPatientHealthChain.ContractDefinition;
using System.Security.Principal;


try
{
    Console.WriteLine("Hello, World!");


    var privateKey = "0x69e8ad2982521b1d34a81fd06cd99360e91a297ca91322405e382c6c619eb741";
    var chainid = 1337;
    var account = new Account(privateKey, chainid);
    var web3 = new Web3(account, "http://127.0.0.1:7545");
    Console.WriteLine(account.Address.ToString());

    web3.Eth.TransactionManager.UseLegacyAsDefault = true;
    NFTPatientHealthChainDeployment healthChainDeployment = new NFTPatientHealthChainDeployment();
    healthChainDeployment.InitialOwner = account.Address;
    var respose = await NFTPatientHealthChainService.DeployContractAndWaitForReceiptAsync(web3, healthChainDeployment);

    Console.WriteLine(respose.ContractAddress.ToString());
}
catch (Exception ex)
{
    Console.WriteLine("Error: " + ex.Message);
}

