// See https://aka.ms/new-console-template for more information
using Nethereum.Hex.HexTypes;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using SmartContract.Contracts.HealthChain;
using SmartContract.Contracts.HealthChain.ContractDefinition;
using System.Net;


Console.WriteLine("Hello, World!");
var privateKey = "0x6223dda46390d86ed29a31b2cfc4ee2a8451c6d90b77cda1879a982a56ab809f";
var chainid = 1337;
var account = new Account(privateKey, chainid);
var web3 = new Web3(account, "http://127.0.0.1:7545");

 Console.WriteLine(account.Address.ToString());

web3.Eth.TransactionManager.UseLegacyAsDefault = true;

var deploymentMessage = new HealthChainDeployment();

var deploymentHandler = web3.Eth.GetContractDeploymentHandler<HealthChainDeployment>();

var gas = new HexBigInteger(2000000);  // Establece un límite de gas adecuado para el despliegue del contrato
deploymentMessage.Gas = gas;

var transactionHash = await deploymentHandler.SendRequestAsync(deploymentMessage);

// Esperar a que la transacción se mine
var receipt = await web3.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(transactionHash);

// Verificar si la transacción se ha minado y el contrato se ha desplegado
if (receipt.Status.Value == 1) // 1 significa éxito en Ethereum
{
    var contractAddress = receipt.ContractAddress;
    Console.WriteLine("Deployed Contract address is: " + contractAddress);
}
else
{
    Console.WriteLine("El despliegue del contrato ha fallado.");
}



//HealthChainDeployment healthChainDeployment = new HealthChainDeployment();
//var respose = await HealthChainService.DeployContractAndWaitForReceiptAsync(web3, healthChainDeployment);

//Console.WriteLine(respose.ContractAddress.ToString());