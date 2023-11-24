// See https://aka.ms/new-console-template for more information
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using SmartContract.Contracts.NFTPatientHealthChain;
using SmartContract.Contracts.NFTPatientHealthChain.ContractDefinition;
using System.Security.Principal;


try
{
    Console.WriteLine("Hello, World!");


    var privateKey = "fdc83fd884ae517f4772c462f794f254f2c499430e265ca724310ecd99b39f62";
    var chainid = 5;
    var account = new Account(privateKey, chainid);
    var web3 = new Web3(account, "https://goerli.infura.io/v3/bdefe16b41da472991a8439fc5398b5d");
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

