---
external help file: Microsoft.Azure.Commands.Sql.dll-Help.xml
Module Name: AzureRM.Sql
online version: https://docs.microsoft.com/en-us/powershell/module/azurerm.sql/get-azurermsqldatabaseautomatictuningconfiguration
schema: 2.0.0
---

# Get-AzureRmSqlDatabaseAutomaticTuningConfiguration

## SYNOPSIS
Gets current Automatic Tuning settings from an Azure SQL Database.

## SYNTAX

```
Get-AzureRmSqlDatabaseAutomaticTuningConfiguration [-ServerName] <String> [-DatabaseName] <String>
 [-ResourceGroupName] <String> [-DefaultProfile <IAzureContextContainer>] [<CommonParameters>]
```

## DESCRIPTION
The **Get-AzureRmSqlDatabaseAutomaticTuningConfiguration** cmdlet gets current Automatic Tuning settings from an Azure SQL Database.

## EXAMPLES

### Example 1
```powershell
PS C:\> Get-AzureRmSqlDatabaseAutomaticTuningConfiguration -ServerName "server01" -ResourceGroupName "resourcegroup01" -DatabaseName "database01"
ResourceId                    : /subscriptions/741fd0f5-9cb8-442c-91c3-3ef4ca231c2a/resourceGroups/resourcegroup01/providers/Microsoft.Sql/servers/server01/databases/database01/automaticTuning/current
ResourceGroupName             : resourcegroup01
ServerName                    : server01
DatabaseName                  : database01
ActualState                   : Auto
DesiredState                  : Auto
ForceLastGoodPlanActualState  : On
ForceLastGoodPlanDesiredState : Default
CreateIndexActualState        : On
CreateIndexDesiredState       : On
DropIndexActualState          : Off
DropIndexDesiredState         : Off
```

Example of getting Automatic Tuning settings from an Azure SQL Database.

## PARAMETERS

### -DatabaseName
The name of the Azure SQL Database to use.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: True
Position: 2
Default value: None
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

### -DefaultProfile
The credentials, account, tenant, and subscription used for communication with Azure.

```yaml
Type: IAzureContextContainer
Parameter Sets: (All)
Aliases: AzureRmContext, AzureCredential

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ResourceGroupName
The name of the resource group.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

### -ServerName
The name of the Azure SQL Server to use.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: True
Position: 1
Default value: None
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see about_CommonParameters (http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### None
This cmdlet does not accept any input.

## OUTPUTS

### Microsoft.Azure.Commands.Sql.AutomaticTuning.Model.AzureSqlDatabaseAutomaticTuningModel

## NOTES

## RELATED LINKS
