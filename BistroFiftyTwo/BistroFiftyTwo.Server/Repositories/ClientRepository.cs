using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BistroFiftyTwo.Server.Entities;
using BistroFiftyTwo.Server.Services;

namespace BistroFiftyTwo.Server.Repositories
{
    public class ClientRepository : IDataRepository<Client>
    {
        public ClientRepository(IConfigurationService configurationService)
        {
            Connection = new NpgsqlConnection(configurationService.Get("Data:IdentityConnection:ConnectionString"));
            Connection.Open();
        }

        protected NpgsqlConnection Connection { get; set; }

        public async Task<Client> Get(Guid id)
        {
            var sql = @"select  id, 
                                 enabled,
                                 protocoltype,
                                 requireclientsecret,
                                 clientname,
                                 clienturi,
                                 logouri,
                                 requireconsent,
                                 allowrememberconsent,
                                 requirepkce,
                                 allowplaintextpkce,
                                 allowaccesstokensviabrowser,
                                 logouturi,
                                 logoutsessionrequired,
                                 allowofflineaccess,
                                 alwaysincludeuserclaimsinidtoken,
                                 identitytokenlifetime,
                                 accesstokenlifetime,
                                 authorizationcodelifetime,
                                 absoluterefreshtokenlifetime,
                                 slidingrefreshtokenlifetime,
                                 refreshtokenusage,
                                 updateaccesstokenclaimsonrefresh,
                                 refreshtokenexpiration,
                                 accesstokentype,
                                 enablelocallogin,
                                 includejwtid,
                                 alwayssetndclientclaims,
                                 prefixclientclaims,
                                 createddate,
                                 createdby,
                                 modifieddate,
                                 modifiedby
                                from clients where id = @id";

            return await Connection.QuerySingleAsync<Client>(sql, new {id});
        }

        public async Task<Client> Create(Client item)
        {
            var parameter = CreateParametersFromEntity(item);
            var columns = @"enabled,
                             protocoltype,
                             requireclientsecret,
                             clientname,
                             clienturi,
                             logouri,
                             requireconsent,
                             allowrememberconsent,
                             requirepkce,
                             allowplaintextpkce,
                             allowaccesstokensviabrowser,
                             logouturi,
                             logoutsessionrequired,
                             allowofflineaccess,
                             alwaysincludeuserclaimsinidtoken,
                             identitytokenlifetime,
                             accesstokenlifetime,
                             authorizationcodelifetime,
                             absoluterefreshtokenlifetime,
                             slidingrefreshtokenlifetime,
                             refreshtokenusage,
                             updateaccesstokenclaimsonrefresh,
                             refreshtokenexpiration,
                             accesstokentype,
                             enablelocallogin,
                             includejwtid,
                             alwayssetndclientclaims,
                             prefixclientclaims,
                             createddate,
                             createdby,
                             modifieddate,
                             modifiedby,";

            var paramlist = @" @enabled,
                                 @protocoltype,
                                 @requireclientsecret,
                                 @clientname,
                                 @clienturi,
                                 @logouri,
                                 @requireconsent,
                                 @allowrememberconsent,
                                 @requirepkce,
                                 @allowplaintextpkce,
                                 @allowaccesstokensviabrowser,
                                 @logouturi,
                                 @logoutsessionrequired,
                                 @allowofflineaccess,
                                 @alwaysincludeuserclaimsinidtoken,
                                 @identitytokenlifetime,
                                 @accesstokenlifetime,
                                 @authorizationcodelifetime,
                                 @absoluterefreshtokenlifetime,
                                 @slidingrefreshtokenlifetime,
                                 @refreshtokenusage,
                                 @updateaccesstokenclaimsonrefresh,
                                 @refreshtokenexpiration,
                                 @accesstokentype,
                                 @enablelocallogin,
                                 @includejwtid,
                                 @alwayssetndclientclaims,
                                 @prefixclientclaims,
                                 @createddate,
                                 @createdby,
                                 @modifieddate,
                                 @modifiedby,";
            var insertQuery = $"insert into clients (${columns}) values (${paramlist}) returning *";

            return await Connection.QuerySingleAsync<Client>(insertQuery, parameter);
        }

        public async Task<IEnumerable<Client>> GetAll()
        {
            var sql = @"select  id, 
                                 enabled,
                                 protocoltype,
                                 requireclientsecret,
                                 clientname,
                                 clienturi,
                                 logouri,
                                 requireconsent,
                                 allowrememberconsent,
                                 requirepkce,
                                 allowplaintextpkce,
                                 allowaccesstokensviabrowser,
                                 logouturi,
                                 logoutsessionrequired,
                                 allowofflineaccess,
                                 alwaysincludeuserclaimsinidtoken,
                                 identitytokenlifetime,
                                 accesstokenlifetime,
                                 authorizationcodelifetime,
                                 absoluterefreshtokenlifetime,
                                 slidingrefreshtokenlifetime,
                                 refreshtokenusage,
                                 updateaccesstokenclaimsonrefresh,
                                 refreshtokenexpiration,
                                 accesstokentype,
                                 enablelocallogin,
                                 includejwtid,
                                 alwayssetndclientclaims,
                                 prefixclientclaims,
                                 createddate,
                                 createdby,
                                 modifieddate,
                                 modifiedby
                                from clients";

            return await Connection.QueryAsync<Client>(sql);
        }

        public async Task<Client> Update(Client item)
        {
            var parameters = CreateParametersFromEntity(item);

            var sql = @"update clients set 
                            enabled = @enabled,
                            protocoltype = @protocoltype,
                            requireclientsecret = @requireclientsecret,
                            clientname = @clientname,
                            clienturi = @clienturi,
                            logouri = @logouri,
                            requireconsent = @requireconsent,
                            allowrememberconsent = @allowrememberconsent,
                            requirepkce = @requirepkce,
                            allowplaintextpkce = @allowplaintextpkce,
                            allowaccesstokensviabrowser = @allowaccesstokensviabrowser,
                            logouturi = @logouturi,
                            logoutsessionrequired = @logoutsessionrequired,
                            allowofflineaccess = @allowofflineaccess,
                            alwaysincludeuserclaimsinidtoken = @alwaysincludeuserclaimsinidtoken,
                            identitytokenlifetime = @identitytokenlifetime,
                            accesstokenlifetime = @accesstokenlifetime,
                            authorizationcodelifetime = @authorizationcodelifetime,
                            absoluterefreshtokenlifetime = @absoluterefreshtokenlifetime,
                            slidingrefreshtokenlifetime = @slidingrefreshtokenlifetime,
                            refreshtokenusage = @refreshtokenusage,
                            updateaccesstokenclaimsonrefresh = @updateaccesstokenclaimsonrefresh,
                            refreshtokenexpiration = @refreshtokenexpiration,
                            accesstokentype = @accesstokentype,
                            enablelocallogin = @enablelocallogin,
                            includejwtid = @includejwtid,
                            alwayssendclientclaims = @alwayssendclientclaims,
                            prefixclientclaims = @prefixclientclaims,
                            modifieddate = @modifieddate,
                            modifiedby = @modifiedby
                    where id = @id
                    returning *";

            return await Connection.QuerySingleAsync<Client>(sql, parameters);
        }

        public async Task Delete(Client item)
        {
            await Connection.ExecuteAsync("delete from clients where id = @id", new {id = item.ID});
        }

        private static object CreateParametersFromEntity(Client item)
        {
            return new
            {
                id = item.ID,
                enabled = item.Enabled,
                protocoltype = item.ProtocolType,
                requireclientsecret = item.RequireClientSecret,
                clientname = item.ClientName,
                clienturi = item.ClientUri,
                logouri = item.LogoUri,
                requireconsent = item.RequireConsent,
                allowrememberconsent = item.AllowRememberConsent,
                requirepkce = item.RequirePkce,
                allowplaintextpkce = item.AllowPlainTextPkce,
                allowaccesstokensviabrowser = item.AllowAccessTokensViaBrowser,
                logouturi = item.LogoutUri,
                logoutsessionrequired = item.LogoutSessionRequired,
                allowofflineaccess = item.AllowOfflineAccess,
                alwaysincludeuserclaimsinidtoken = item.AlwaysIncludeUserClaimsInIdToken,
                identitytokenlifetime = item.IdentityTokenLifetime,
                accesstokenlifetime = item.AccessTokenLifetime,
                authorizationcodelifetime = item.AuthorizationCodeLifetime,
                absoluterefreshtokenlifetime = item.AbsoluteRefreshTokenLifetime,
                slidingrefreshtokenlifetime = item.SlidingRefreshTokenLifetime,
                refreshtokenusage = item.RefreshTokenUsage,
                updateaccesstokenclaimsonrefresh = item.UpdateAccessTokenClaimsOnRefresh,
                refreshtokenexpiration = item.RefreshTokenExpiration,
                accesstokentype = item.AccessTokenType,
                enablelocallogin = item.EnableLocalLogin,
                includejwtid = item.IncludeJwtId,
                alwayssendclientclaims = item.AlwaysSendClientClaims,
                prefixclientclaims = item.PrefixClientClaims,
                createddate = item.CreatedDate,
                createdby = item.CreatedBy,
                modifieddate = item.ModifiedDate,
                modifiedby = item.ModifiedBy
            };
        }
    }
}