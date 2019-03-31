# CoinField.Api
A .Net Standard client for the CoinField cryptocurrency API.

This version covers all public methods of the [REST API V1](https://api.coinfield.com/v1/docs/).

[![nuget](https://img.shields.io/nuget/v/CoinField.Api.svg)](https://www.nuget.org/packages/CoinField.Api/)
![Downloads](https://img.shields.io/nuget/dt/CoinField.Api.svg)

An account is not required to access the public API methods. 
However, if you do create an account, please use my affiliate link when you register.
It's an easy way to give back to this project at no cost to you: 
https://coinfield.com/ref/0/ID159F0248CB

<a href="https://coinfield.com/ref/0/ID159F0248CB" target="_blank"><img src="https://s3.ca-central-1.amazonaws.com/coinfield-marketing/october-4/300x250.png" alt="Coinfield Digitial Exchange" width="336" height="280" border="0"></a>


## Installation via NuGet
```
Install-Package CoinField.Api
```

## Example usage

```csharp
using CoinField.Api;
using System;
using System.Linq;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello CoinField!");

            using (var client = new CoinFieldClient())
            {
                // Get the status to test connectivity.
                var statusResult = client.GetStatusAsync().Result;

                // Get the timestamp of the server.
                var timeResult = client.GetTimestampAsync().Result;

                Console.WriteLine("Status is '{0}' at {1}.", statusResult.Status, timeResult.Timestamp);

                ShowCurrencies(client);

                ShowMarkets(client);

                ShowTickers(client);

                // From this point, let's explore the BitCoin - Canadian Dollar market.
                var market = "btccad";

                ShowOrderBook(client, market);

                ShowDepth(client, market);

                ShowOhlc(client, market);

                ShowTrades(client, market);
            }

            // Keep the console window open.
            Console.WriteLine("\nPress any key to exit.");
            Console.ReadKey();
        }

        private static void ShowCurrencies(CoinFieldClient client)
        {
            var result = client.GetCurrenciesAsync().Result;

            Console.WriteLine("\nGot {0} currencies in {1}.\n", result.Currencies.Count(), result.Took);

            Console.WriteLine($"{"Id",5} |{"Type",10} |{"Erc20",5} |{"Name",16} |{"Symbol",6} |{"ISO 4217",8} |{ "Precision",5}");
            Console.WriteLine(new string('-', 72));

            foreach (var currency in result.Currencies)
            {
                Console.WriteLine($"{currency.Id,5} |" +
                               $"{currency.Type,10} |" +
                               $"{currency.Erc20,5} |" +
                               $"{currency.Name,16} |" +
                               $"{currency.Symbol,6} |" +
                               $"{currency.Iso4217,8} |" +
                               $"{currency.Precision,5}");
            }
        }

        private static void ShowMarkets(CoinFieldClient client)
        {
            var result = client.GetMarketsAsync().Result;

            Console.WriteLine("\nGot {0} markets in {1}.\n", result.Markets.Count(), result.Took);

            Console.WriteLine($"{"Id",10} |{"Name",10} |{"Ask Prc",10} |{"Bid Prc",10} |{"Min Funds",10} |{"Max Funds",10} |{ "Min Volume",10} |{ "Max Volume",10}");
            Console.WriteLine(new string('-', 95));

            foreach (var market in result.Markets)
            {
                Console.WriteLine($"{market.Id,10} |" +
                               $"{market.Name,10} |" +
                               $"{market.AskPrecision,10} |" +
                               $"{market.BidPrecision,10} |" +
                               $"{market.MinimumFunds,10} |" +
                               $"{market.MaximumFunds,10} |" +
                               $"{market.MinimumVolume,10} |" +
                               $"{market.MaximumVolume,10}");
            }
        }

        private static void ShowTickers(CoinFieldClient client)
        {
            var result = client.GetTickersAsync().Result;

            Console.WriteLine("\nGot {0} tickers in {1}.\n", result.Markets.Count(), result.Took);

            Console.WriteLine($"{"Market",10} |{"Bid",10} |{"Ask",10} |{"Low",10} |{"High",10} |{ "Last",10} |{ "Open",10} |{ "Volume",10}");
            Console.WriteLine(new string('-', 95));

            foreach (var market in result.Markets)
            {
                Console.WriteLine($"{market.Market,10} |" +
                               $"{market.Bid,10} |" +
                               $"{market.Ask,10} |" +
                               $"{market.Low,10} |" +
                               $"{market.High,10} |" +
                               $"{market.Last,10} |" +
                               $"{market.Open,10} |" +
                               $"{market.Vol,10}");
            }
        }

        private static void ShowOrderBook(CoinFieldClient client, string market)
        {
            var result = client.GetOrderBookAsync(market).Result;

            Console.WriteLine("\nGot order book for '{0}' in {1}.\n", result.Market, result.Took);

            Console.WriteLine("Total volume of asks is {0} with hash {1}", result.TotalAsks, result.AsksHash);
            Console.WriteLine($"{"Id",20} |{"Price",10} |{"Volume",10} |{"Timestamp", 25}");
            Console.WriteLine(new string('-', 75));

            foreach (var ask in result.Asks.OrderByDescending(a=> a.Price))
            {
                Console.WriteLine($"{ask.Id,20} |" +
                               $"{ask.Price,10} |" +
                               $"{ask.Volume,10} |" +
                               $"{ask.Timestamp,25}");
            }

            Console.WriteLine();

            Console.WriteLine("Total volume of bids is {0} with hash {1}", result.TotalBids, result.BidsHash);
            Console.WriteLine($"{"Id",20} |{"Price",10} |{"Volume",10} |{"Timestamp",25}");
            Console.WriteLine(new string('-', 75));

            foreach (var bid in result.Bids.OrderByDescending(b=> b.Price))
            {
                Console.WriteLine($"{bid.Id,20} |" +
                               $"{bid.Price,10} |" +
                               $"{bid.Volume,10} |" +
                               $"{bid.Timestamp,25}");
            }
        }

        private static void ShowDepth(CoinFieldClient client, string market)
        {
            var result = client.GetDepthAsync(market).Result;
            decimal sum;

            Console.WriteLine("\nGot depth for '{0}' in {1}.\n", result.Market, result.Took);

            Console.WriteLine($"{"Price",10} |{"Volume",15} |{"Sum Asks",10}");
            Console.WriteLine(new string('-', 40));
            sum = 0;

            foreach (var ask in result.Asks.OrderBy(a => a.Price))
            {
                sum += Convert.ToDecimal(ask.Volume);
                Console.WriteLine($"{ask.Price,10} |" + $"{ask.Volume,15}|" + $"{sum,10}");
            }

            Console.WriteLine();

            Console.WriteLine($"{"Price",10} |{"Volume",15} |{"Sum Bids",10}");
            Console.WriteLine(new string('-', 40));
            sum = 0;

            foreach (var bid in result.Bids.OrderByDescending(b => b.Price))
            {
                sum += Convert.ToDecimal(bid.Volume);
                Console.WriteLine($"{bid.Price,10} |" + $"{bid.Volume,15}|" + $"{sum,10}");
            }
        }

        private static void ShowOhlc(CoinFieldClient client, string market)
        {
            var result = client.GetOhlcAsync(market).Result;

            Console.WriteLine("\nGot OHLC for '{0}' in {1}.\n", result.Market, result.Took);

            Console.WriteLine($"{"Timestamp",25} |{"Open",10} |{"High",10} |{"Low",10} |{"Close",10} |{ "Volume",10}");
            Console.WriteLine(new string('-', 85));

            foreach (var line in result.KLines)
            {
                Console.WriteLine($"{line.Timestamp,25} |" + 
                    $"{ line.Open,10} |" + 
                    $"{line.High,10} |" + 
                    $"{line.Low,10} |" + 
                    $"{line.Close,10} |" + 
                    $"{line.Volume,10}");
            }
        }

        private static void ShowTrades(CoinFieldClient client, string market)
        {
            var result = client.GetTradesAsync(market).Result;

            Console.WriteLine("\nGot trades for '{0}' in {1}.\n", result.Market, result.Took);

            Console.WriteLine($"{"Id",20} |{"Price",10} |{"Volume",10} |{"Total Value",15} |{"Timestamp",25}");
            Console.WriteLine(new string('-', 90));

            foreach (var trade in result.Trades.OrderByDescending(t => t.Timestamp))
            {
                Console.WriteLine($"{trade.Id,20} |" +
                               $"{trade.Price,10} |" +
                               $"{trade.Volume,10} |" +
                               $"{trade.TotalValue,15} |" +
                               $"{trade.Timestamp,25}");
            }
        }
    }
}

```

### Output
```
Hello CoinField!
Status is 'ok' at 2018-11-03T21:44:15.702Z.

Got 21 currencies in 0ms.

   Id |      Type |Erc20 |            Name |Symbol |ISO 4217 |Precision
------------------------------------------------------------------------
  cad |      fiat |False |Canadian Dollars |    C$ |     CAD |    2
  usd |      fiat |False |      US Dollars |     $ |     USD |    2
  eur |      fiat |False |            Euro |     ? |     EUR |    2
  jpy |      fiat |False |    Japanese Yen |     ¥ |     JPY |    2
  gbp |      fiat |False |  British Pounds |     £ |     GBP |    2
  aed |      fiat |False |     UAE Dirhams |   AED |     AED |    2
  xrp |    crypto |False |             XRP |   XRP |         |    8
  btc |    crypto |False |         Bitcoin |     ? |     XBT |    8
  eth |    crypto |False |        Ethereum |     ? |         |    8
  ltc |    crypto |False |        Litecoin |     L |         |    8
 dash |    crypto |False |            Dash |     D |         |    8
  bch |    crypto |False |    Bitcoin Cash |   BCH |         |    8
  btg |    crypto |False |    Bitcoin Gold |   BTG |         |    8
  zec |    crypto |False |           Zcash |   ZEC |         |    8
  zrx |    crypto | True |              0x |   ZRX |         |    8
  gnt |    crypto | True |           Golem |   GNT |         |    8
  rep |    crypto | True |           Augur |   REP |         |    8
  omg |    crypto | True |         OmiseGO |   OMG |         |    8
 salt |    crypto | True |            Salt |  SALT |         |    8
  bat |    crypto | True |             BAT |   BAT |         |    8
  zil |    crypto | True |         Zilliqa |   ZIL |         |    8

Got 29 markets in 0ms.

        Id |      Name |   Ask Prc |   Bid Prc | Min Funds | Max Funds |Min Volume |Max Volume
-----------------------------------------------------------------------------------------------
    btcxrp |   BTC/XRP |         8 |         4 |      20.0 |  500000.0 |     0.001 |      50.0
    ethxrp |   ETH/XRP |         4 |         4 |      20.0 |  500000.0 |       0.1 |     300.0
   dashxrp |  DASH/XRP |         3 |         4 |      20.0 |  500000.0 |       0.1 |     500.0
    ltcxrp |   LTC/XRP |         2 |         4 |      20.0 |  500000.0 |       0.1 |    1000.0
    bchxrp |   BCH/XRP |         3 |         4 |      20.0 |  500000.0 |      0.05 |     200.0
    zecxrp |   ZEC/XRP |         3 |         4 |      20.0 |  500000.0 |       0.1 |     500.0
    btgxrp |   BTG/XRP |         2 |         4 |      20.0 |  500000.0 |       0.5 |    2000.0
    zrxxrp |   ZRX/XRP |         1 |         4 |      20.0 |  500000.0 |      10.0 |  100000.0
    gntxrp |   GNT/XRP |         1 |         4 |      20.0 |  500000.0 |      10.0 |  100000.0
    repxrp |   REP/XRP |         3 |         4 |      20.0 |  500000.0 |       1.0 |    2000.0
    omgxrp |   OMG/XRP |         2 |         4 |      20.0 |  500000.0 |       3.0 |    5000.0
   saltxrp |  SALT/XRP |         2 |         4 |      20.0 |  500000.0 |      10.0 |  100000.0
    batxrp |   BAT/XRP |         1 |         4 |      20.0 |  500000.0 |      50.0 |  200000.0
    zilxrp |   ZIL/XRP |         1 |         4 |      20.0 |  500000.0 |     300.0 |  999999.0
    xrpcad |   XRP/CAD |         4 |         4 |      10.0 |  300000.0 |      20.0 |  200000.0
    xrpusd |   XRP/USD |         4 |         4 |      10.0 |  300000.0 |      20.0 |  200000.0
    xrpeur |   XRP/EUR |         4 |         4 |      10.0 |  300000.0 |      20.0 |  200000.0
    xrpgbp |   XRP/GBP |         4 |         4 |      10.0 |  300000.0 |      20.0 |  200000.0
    xrpaed |   XRP/AED |         4 |         4 |      50.0 |  999999.0 |      20.0 |  200000.0
    xrpjpy |   XRP/JPY |         4 |         2 |    1500.0 |           |      20.0 |  200000.0
    btceur |   BTC/EUR |         8 |         2 |      10.0 |  300000.0 |     0.001 |      50.0
    etheur |   ETH/EUR |         4 |         2 |      10.0 |  300000.0 |       0.1 |     300.0
    ltceur |   LTC/EUR |         2 |         2 |      10.0 |  300000.0 |       0.1 |    1000.0
    btccad |   BTC/CAD |         8 |         2 |      10.0 |  300000.0 |     0.001 |      50.0
    ethcad |   ETH/CAD |         4 |         2 |      10.0 |  300000.0 |       0.1 |     300.0
    ltccad |   LTC/CAD |         2 |         2 |      10.0 |  300000.0 |       0.1 |    1000.0
    btcusd |   BTC/USD |         8 |         2 |      10.0 |  300000.0 |     0.001 |      50.0
    ethusd |   ETH/USD |         4 |         2 |      10.0 |  300000.0 |       0.1 |     300.0
    ltcusd |   LTC/USD |         2 |         2 |      10.0 |  300000.0 |       0.1 |    1000.0

Got 29 tickers in 10ms.

    Market |       Bid |       Ask |       Low |      High |      Last |      Open |    Volume
-----------------------------------------------------------------------------------------------
    btcxrp |  14273.52 |  14279.51 |  13727.21 |  14571.33 |  14279.51 |  14178.72 |  46.91251
    ethxrp |  448.4054 |       450 |  431.5369 |  450.0956 |  449.2476 |  448.3352 |  479.6699
   dashxrp |  348.6914 |  349.5387 |   343.897 |  354.2263 |   349.045 |   343.994 |   176.164
    ltcxrp |  114.9589 |  115.2536 |  113.8889 |  117.2143 |  115.1178 |  114.4942 |    438.88
    bchxrp |  1085.327 |    1086.2 |  1023.921 |  1086.435 |  1085.764 |  1049.381 |   167.979
    zecxrp |   262.592 |  262.8915 |   261.059 |  264.9063 |   262.794 |   264.526 |   167.274
    btgxrp |   57.9929 |   61.3054 |   59.4447 |   61.9138 |   59.6551 |   60.9575 |   1987.71
    zrxxrp |    1.7944 |    1.8025 |    1.7898 |    1.8454 |    1.7986 |    1.8445 |   66718.5
    gntxrp |    0.3831 |    0.3838 |    0.3818 |    0.3882 |    0.3834 |    0.3868 |  178090.5
    repxrp |   34.6852 |   34.7071 |    32.473 |    34.696 |    34.693 |   32.6428 |  1351.105
    omgxrp |    7.1959 |    7.2462 |     6.919 |    7.3035 |    7.2196 |    7.2822 |   6703.51
   saltxrp |    1.6366 |    1.6503 |    1.6005 |    1.7624 |    1.6433 |    1.6131 |  15031.24
    batxrp |    0.6542 |    0.6637 |    0.6339 |    0.7411 |    0.6583 |    0.7402 |  143563.2
    zilxrp |    0.0786 |    0.0789 |    0.0759 |    0.0802 |    0.0787 |    0.0794 |   1319301
    xrpcad |    0.6076 |     0.612 |    0.6057 |    0.6232 |    0.6098 |    0.6174 |  703631.8
    xrpusd |    0.4656 |    0.4674 |    0.4651 |    0.4759 |    0.4656 |    0.4715 |  673033.2
    xrpeur |     0.408 |    0.4112 |    0.4092 |    0.4189 |    0.4096 |    0.4146 |  578611.6
    xrpgbp |    0.3572 |      0.36 |    0.3503 |    0.3666 |    0.3586 |    0.3631 |  483945.3
    xrpaed |    1.7022 |    1.7152 |    1.7063 |    1.7302 |     1.709 |    1.7294 |  407465.6
    xrpjpy |     52.51 |      52.9 |     52.64 |      53.9 |     52.69 |     53.18 |  476827.9
    btceur |   5679.78 |   5737.15 |   5703.26 |   5834.78 |   5709.04 |   5729.44 |  58.34494
    etheur |       179 |    180.05 |    178.97 |    183.07 |    179.54 |    180.56 |  355.4568
    ltceur |     45.84 |     45.97 |     45.72 |     46.92 |     45.91 |     46.33 |    382.94
    btccad |      8500 |   8554.02 |   8495.95 |   8678.85 |   8528.72 |   8525.77 |  86.38633
    ethcad |    266.67 |    268.02 |    266.39 |    272.65 |    267.32 |    270.23 |  449.2922
    ltccad |     68.22 |     68.41 |     68.06 |     69.76 |     68.31 |     68.99 |    506.38
    btcusd |      6500 |   6519.49 |   6500.31 |   6629.44 |   6508.44 |   6509.43 |   78.3633
    ethusd |    203.59 |    204.58 |    203.35 |    208.12 |    204.09 |     205.7 |  460.8464
    ltcusd |     52.13 |         0 |     50.64 |     52.92 |     52.33 |     52.62 |    323.96

Got order book for 'btccad' in 0ms.

Total volume of asks is 3.18403715 with hash 28e1bc754307f263c4c0eaaaf9fbb8e8
                  Id |     Price |    Volume |                Timestamp
---------------------------------------------------------------------------
  pd111dtrjpl001pe7h |    8700.0 |0.16950358 |    2018-11-03 4:39:17 PM
  pd111dtrsc2001pg73 |    8687.7 |0.00176602 |    2018-11-03 7:05:38 PM
  pd111dts1dp001ph7q |   8683.52 | 0.0020514 |    2018-11-03 8:31:53 PM
  pd111dtrsc2001pg7s |   8678.15 | 0.0028565 |    2018-11-03 7:05:38 PM
  pd111dtrsbr001pg58 |    8676.4 |0.00207886 |    2018-11-03 7:05:31 PM
  pd111dts1dp001ph7r |   8669.65 |0.15976554 |    2018-11-03 8:31:53 PM
  pd111dtrsc2001pg6p |   8666.02 |1.33042424 |    2018-11-03 7:05:38 PM
  pd111dtrsbr001pg57 |   8659.94 |0.00312635 |    2018-11-03 7:05:31 PM
  pd111dtrsc2001pg7f |   8654.77 |0.00651062 |    2018-11-03 7:05:38 PM
  pd111dts1dp001ph7s |   8652.34 |0.00816768 |    2018-11-03 8:31:53 PM
  pd111dtrsbr001pg51 |   8650.43 |0.00286515 |    2018-11-03 7:05:31 PM
  pd111dtrsc3001pg81 |   8640.08 |0.20484139 |    2018-11-03 7:05:39 PM
  pd111dtrsbr001pg55 |   8637.47 |0.94848854 |    2018-11-03 7:05:31 PM
  pd111dts44c001php8 |    8628.0 |  0.002994 |    2018-11-03 9:18:04 PM
  pd111dts3po001phn3 |   8620.09 |0.30956066 |    2018-11-03 9:12:24 PM
  pd111dts3po001phn5 |   8606.32 |0.00500581 |    2018-11-03 9:12:24 PM
  pd111dts3po001phn4 |   8592.57 |0.00535776 |    2018-11-03 9:12:24 PM
  pd111dts3po001phn0 |   8572.85 | 0.0070375 |    2018-11-03 9:12:24 PM
  pd111dts3po001phn2 |   8564.29 |0.00722647 |    2018-11-03 9:12:24 PM
  pd111dts3po001phn1 |   8554.02 |0.00440908 |    2018-11-03 9:12:24 PM

Total volume of bids is 0.4031726 with hash 24fb26865fa70935f4557aec63765bd8
                  Id |     Price |    Volume |                Timestamp
---------------------------------------------------------------------------
  pd211dtq94m001p525 |    8500.0 | 0.2159004 |    2018-11-03 4:31:18 AM
  pd211dtrsbr001pg59 |   8455.12 |  0.004074 |    2018-11-03 7:05:31 PM
  pd211dtrsc2001pg5n |   8455.12 | 0.0041732 |    2018-11-03 7:05:38 PM
  pd211dtrsbr001pg4v |   8124.01 | 0.0035094 |    2018-11-03 7:05:31 PM
  pd211dtrsc2001pg64 |   8124.01 | 0.0170562 |    2018-11-03 7:05:38 PM
  pd211dtrsc2001pg5o |    8116.7 | 0.0100614 |    2018-11-03 7:05:38 PM
  pd211dtrsc2001pg5i |   8105.34 | 0.0051546 |    2018-11-03 7:05:38 PM
  pd211dtrsbr001pg5a |   8104.52 | 0.0067212 |    2018-11-03 7:05:31 PM
  pd211dtrsc2001pg5l |   8097.23 |  0.009453 |    2018-11-03 7:05:38 PM
  pd211dtrsbr001pg52 |   8083.45 |  0.003939 |    2018-11-03 7:05:31 PM
  pd211dtrsc2001pg5k |    8077.8 |  0.015747 |    2018-11-03 7:05:38 PM
  pd211dtrsc2001pg7i |   8060.84 | 0.0160662 |    2018-11-03 7:05:38 PM
  pd211dtrsc2001pg5t |   8048.75 | 0.0030708 |    2018-11-03 7:05:38 PM
  pd211dtrsc2001pg5p |   8031.84 | 0.0164778 |    2018-11-03 7:05:38 PM
  pd211dtrsc2001pg67 |   8023.81 | 0.0042012 |    2018-11-03 7:05:38 PM
  pd211dtrsc2001pg62 |   8009.37 | 0.0166944 |    2018-11-03 7:05:38 PM
  pd211dtrsc2001pg5s |   7996.55 | 0.0173496 |    2018-11-03 7:05:38 PM
  pd211dtrsc2001pg5m |   7986.16 |  0.007749 |    2018-11-03 7:05:38 PM
  pd211dtrsc2001pg5r |   7970.19 | 0.0123354 |    2018-11-03 7:05:38 PM
  pd211dtrsc2001pg63 |   7955.04 | 0.0134388 |    2018-11-03 7:05:38 PM

Got depth for 'btccad' in 22ms.

     Price |         Volume |  Sum Asks
----------------------------------------
   8554.02 |     0.00440908|0.00440908
   8564.29 |     0.00722647|0.01163555
   8572.85 |      0.0070375|0.01867305
   8592.57 |     0.00535776|0.02403081
   8606.32 |     0.00500581|0.02903662
   8620.09 |     0.30956066|0.33859728
    8628.0 |       0.002994|0.34159128
   8637.47 |     0.94848854|1.29007982
   8640.08 |     0.20484139|1.49492121
   8650.43 |     0.00286515|1.49778636
   8652.34 |     0.00816768|1.50595404
   8654.77 |     0.00651062|1.51246466
   8659.94 |     0.00312635|1.51559101
   8666.02 |     1.33042424|2.84601525
   8669.65 |     0.15976554|3.00578079
    8676.4 |     0.00207886|3.00785965
   8678.15 |      0.0028565|3.01071615
   8683.52 |      0.0020514|3.01276755
    8687.7 |     0.00176602|3.01453357
    8700.0 |     0.16950358|3.18403715
   8701.76 |     0.00320546|3.18724261
   8705.94 |     0.00196095|3.18920356
   8717.26 |     0.98492231|4.17412587
    8720.9 |     0.89035257|5.06447844
   8729.46 |     0.97990724|6.04438568
   8729.62 |     0.00404966|6.04843534
    8740.0 |     0.12352113|6.17195647
   8742.56 |     0.00200752|6.17396399
   8768.79 |      0.0085262|6.18249019
   8773.86 |         0.7424|6.92489019
   8792.46 |     0.00442848|6.92931867
   8801.23 |          0.425|7.35431867
   8803.01 |     0.00290694|7.35722561
   8825.02 |     0.88957643|8.24680204
   8835.61 |     0.00600613|8.25280817
   8847.07 |     0.00174755|8.25455572
    8850.0 |     1.12570017|9.38025589
   8855.05 |     0.00528761|9.38554350
   8860.25 |         0.4688|9.85434350
    8863.9 |     0.00801067|9.86235417
   8886.95 |      0.0086268|9.87098097
   8900.28 |     0.00150064|9.87248161
   8926.09 |      0.0077026|9.88018421
    8949.3 |     0.41833973|10.29852394
   8960.04 |     0.00705422|10.30557816
   8981.54 |     0.00191319|10.30749135
   8994.12 |     0.00509148|10.31258283
    9015.7 |      1.4809955|11.79357833
   9024.72 |     0.00391533|11.79749366
   9037.35 |     0.00332905|11.80082271
   9054.52 |     0.00183945|11.80266216
   9066.29 |     0.00558851|11.80825067
   9078.99 |     0.00551448|11.81376515
   9100.78 |     0.00315889|11.81692404
   9121.71 |     0.00419325|11.82111729
   9148.16 |     0.00833276|11.82945005
   9171.95 |     0.00277948|11.83222953
   9187.54 |     0.00518939|11.83741892

     Price |         Volume |  Sum Bids
----------------------------------------
    8500.0 |      0.2159004| 0.2159004
   8455.12 |      0.0082472| 0.2241476
   8124.01 |      0.0205656| 0.2447132
    8116.7 |      0.0100614| 0.2547746
   8105.34 |      0.0051546| 0.2599292
   8104.52 |      0.0067212| 0.2666504
   8097.23 |       0.009453| 0.2761034
   8083.45 |       0.003939| 0.2800424
    8077.8 |       0.015747| 0.2957894
   8060.84 |      0.0160662| 0.3118556
   8048.75 |      0.0030708| 0.3149264
   8031.84 |      0.0164778| 0.3314042
   8023.81 |      0.0042012| 0.3356054
   8009.37 |      0.0166944| 0.3522998
   7996.55 |      0.0173496| 0.3696494
   7986.16 |       0.007749| 0.3773984
   7970.19 |      0.0123354| 0.3897338
   7955.04 |      0.0134388| 0.4031726
   7940.72 |      0.0051282| 0.4083008
    7931.2 |         1.5103| 1.9186008
    7918.0 |         1.1619| 3.0805008
   7910.57 |       0.004131| 3.0846318
   7900.29 |      0.0126438| 3.0972756
   7882.91 |      0.0116064| 3.1088820
    7870.3 |       0.005373| 3.1142550
    7863.0 |          0.111| 3.2252550
   7862.43 |         1.2706| 4.4958550
    7846.7 |       0.017937| 4.5137920
   7827.87 |         1.9986| 6.5123920
   7820.04 |      0.0179352| 6.5303272
   7796.58 |       0.006648| 6.5369752
   7779.43 |         1.6173| 8.1542752
   7759.98 |         2.9519|11.1061752
   7751.44 |      0.0068196|11.1129948
   7739.82 |      0.0168912|11.1298860
   7720.47 |         1.1313|12.2611860
   7712.75 |      0.0071484|12.2683344
   7698.09 |      0.0110676|12.2794020
   7687.32 |      0.0123174|12.2917194
    7680.4 |      0.0133878|12.3051072
   7668.88 |      0.0074592|12.3125664
   7652.01 |         0.8599|13.1724664
   7641.29 |      0.0147078|13.1871742
    7619.9 |      0.0127404|13.1999146
   7606.18 |      0.0120732|13.2119878
   7598.57 |         1.1278|14.3397878
   7590.22 |      0.0167706|14.3565584
   7575.04 |      0.0064782|14.3630366
   7565.19 |      0.0128886|14.3759252
   7544.01 |      0.0117804|14.3877056
   7523.64 |      0.0107358|14.3984414

Got OHLC for 'btccad' in 14ms.

                Timestamp |      Open |      High |       Low |     Close |    Volume
-------------------------------------------------------------------------------------
    2018-11-03 7:15:00 PM |   8521.12 |   8521.12 |   8521.12 |   8521.12 |    0.6935
    2018-11-03 7:20:00 PM |   8521.12 |   8521.12 |   8518.56 |   8518.56 |    0.7553
    2018-11-03 7:25:00 PM |   8518.56 |   8521.12 |   8518.56 |   8521.12 |    0.4809
    2018-11-03 7:30:00 PM |   8521.12 |   8521.12 |   8520.26 |   8520.26 |    0.6211
    2018-11-03 7:35:00 PM |   8520.26 |   8520.26 |   8519.41 |   8519.41 |    0.3911
    2018-11-03 7:40:00 PM |   8519.41 |   8520.26 |   8519.41 |   8520.26 |    0.3933
    2018-11-03 7:45:00 PM |   8520.26 |   8520.26 |   8518.56 |   8518.56 |    0.9732
    2018-11-03 7:50:00 PM |   8518.56 |   8520.26 |   8518.56 |   8520.26 |    0.6496
    2018-11-03 7:55:00 PM |   8520.26 |   8520.26 |   8520.26 |   8520.26 |       0.0
    2018-11-03 8:00:00 PM |   8520.26 |   8520.26 |   8520.26 |   8520.26 |    0.6186
    2018-11-03 8:05:00 PM |   8520.26 |   8520.26 |   8520.26 |   8520.26 |    0.3417
    2018-11-03 8:10:00 PM |   8520.26 |   8520.26 |   8519.41 |   8519.41 |     0.925
    2018-11-03 8:15:00 PM |   8519.41 |   8519.41 |   8519.41 |   8519.41 |    0.7021
    2018-11-03 8:20:00 PM |   8519.41 |   8519.41 |   8519.41 |   8519.41 |       0.0
    2018-11-03 8:25:00 PM |   8519.41 |   8519.41 |   8519.41 |   8519.41 |       0.0
    2018-11-03 8:30:00 PM |   8519.41 |   8637.47 |   8519.41 |   8637.47 | 0.1672323
    2018-11-03 8:35:00 PM |    8524.2 |    8524.2 |    8524.2 |    8524.2 |    0.6826
    2018-11-03 8:40:00 PM |    8524.2 |    8524.2 |   8514.25 |   8514.25 |    0.3555
    2018-11-03 8:45:00 PM |   8525.91 |   8525.91 |   8525.91 |   8525.91 |    0.4248
    2018-11-03 8:50:00 PM |   8525.91 |   8525.91 |    8524.2 |    8524.2 |     0.712
    2018-11-03 8:55:00 PM |    8524.2 |    8524.2 |   8523.35 |   8523.35 |    0.5169
    2018-11-03 9:00:00 PM |   8523.35 |    8524.2 |   8523.35 |    8524.2 |    0.6779
    2018-11-03 9:05:00 PM |    8524.2 |    8524.2 |   8523.35 |   8523.35 |    0.4428
    2018-11-03 9:10:00 PM |   8523.35 |   8637.47 |   8523.35 |   8627.33 |0.77411976
    2018-11-03 9:15:00 PM |   8627.33 |   8627.33 |   8527.01 |   8527.01 |    0.7129
    2018-11-03 9:20:00 PM |   8527.01 |   8527.86 |   8527.01 |   8527.86 |    0.3561
    2018-11-03 9:25:00 PM |   8527.86 |   8527.86 |    8525.3 |    8525.3 |    0.5013
    2018-11-03 9:30:00 PM |    8525.3 |   8527.86 |    8525.3 |   8527.86 |    0.5444
    2018-11-03 9:35:00 PM |    8525.3 |    8525.3 |    8525.3 |    8525.3 |    0.5286
    2018-11-03 9:40:00 PM |    8525.3 |   8528.72 |    8525.3 |   8528.72 |    0.3712

Got trades for 'btccad' in 13ms.

                  Id |     Price |    Volume |    Total Value |                Timestamp
------------------------------------------------------------------------------------------
    pd1dts5if00an1ca |   8528.72 |    0.3712 |    3165.860864 |    2018-11-03 9:42:39 PM
    pd1dts54s00an1c9 |    8525.3 |    0.5286 |     4506.47358 |    2018-11-03 9:35:24 PM
    pd1dts52a00an1c8 |   8527.86 |    0.5444 |    4642.566984 |    2018-11-03 9:34:02 PM
    pd1dts4p700an1c7 |    8525.3 |    0.5013 |     4273.73289 |    2018-11-03 9:29:11 PM
    pd1dts4b100an1c6 |   8527.86 |    0.3561 |    3036.770946 |    2018-11-03 9:21:37 PM
    pd1dts43900an1c5 |   8527.01 |    0.7129 |    6078.905429 |    2018-11-03 9:17:29 PM
    pd1dts3pk00an1c4 |   8627.33 |0.00161378 |  13.9226126074 |    2018-11-03 9:12:20 PM
    pd1dts3pk00an1c3 |   8605.81 |0.00326695 |  28.1147509795 |    2018-11-03 9:12:20 PM
    pd1dts3pk00an1c2 |   8567.21 |0.00395623 |  33.8938532183 |    2018-11-03 9:12:20 PM
    pd1dts3pk00an1c1 |   8550.11 |0.00373682 |  31.9502220502 |    2018-11-03 9:12:20 PM
    pd1dts3pk00an1c0 |   8598.07 | 0.0057718 |   49.626340426 |    2018-11-03 9:12:20 PM
    pd1dts3pk00an1bv |   8637.47 |0.19999079 |1727.4144489013 |    2018-11-03 9:12:20 PM
    pd1dts3pk00an1bu |   8590.34 |0.00408339 |  35.0777084526 |    2018-11-03 9:12:20 PM
    pd1dts3op00an1bt |   8525.06 |    0.5517 |    4703.275602 |    2018-11-03 9:11:53 PM
    pd1dts3k000an1bs |   8523.35 |    0.4428 |     3774.13938 |    2018-11-03 9:09:20 PM
    pd1dts34o00an1br |    8524.2 |    0.6779 |     5778.55518 |    2018-11-03 9:01:12 PM
    pd1dts30d00an1bq |   8523.35 |    0.5169 |    4405.719615 |    2018-11-03 8:58:53 PM
    pd1dts2nj00an1bp |    8524.2 |     0.712 |      6069.2304 |    2018-11-03 8:54:11 PM
    pd1dts27f00an1bo |   8525.91 |    0.4248 |    3621.806568 |    2018-11-03 8:45:35 PM
    pd1dts22d00an1bn |   8514.25 |    0.3555 |    3026.815875 |    2018-11-03 8:42:53 PM
    pd1dts1l200an1bm |    8524.2 |    0.6826 |     5818.61892 |    2018-11-03 8:35:46 PM
    pd1dts1dn00an1bl |   8637.47 |0.08874835 | 766.5612106745 |    2018-11-03 8:31:51 PM
    pd1dts1dn00an1bk |   8622.84 |  0.008065 |     69.5432046 |    2018-11-03 8:31:51 PM
    pd1dts1dn00an1bj |   8621.09 |0.00837575 |  72.2080945675 |    2018-11-03 8:31:51 PM
    pd1dts1dn00an1bi |   8596.16 |0.00522104 |  44.8808952064 |    2018-11-03 8:31:51 PM
    pd1dts1dn00an1bh |   8549.07 |0.00727453 |  62.1904661871 |    2018-11-03 8:31:51 PM
    pd1dts1dn00an1bg |   8588.46 |0.00775812 |  66.6303032952 |    2018-11-03 8:31:51 PM
    pd1dts1dn00an1bf |   8573.88 |0.00892591 |  76.5296812308 |    2018-11-03 8:31:51 PM
    pd1dts1dn00an1be |   8580.72 |0.00781484 |  67.0569538848 |    2018-11-03 8:31:51 PM
    pd1dts1dn00an1bd |   8614.22 |0.00322845 |   27.810578559 |    2018-11-03 8:31:51 PM
    pd1dts1dn00an1bc |   8560.18 | 0.0089295 |    76.43812731 |    2018-11-03 8:31:51 PM
    pd1dts1dn00an1bb |   8540.53 |0.00687452 |  58.7120442956 |    2018-11-03 8:31:51 PM
    pd1dts1dn00an1ba |   8540.53 |0.00273172 |  23.3303366116 |    2018-11-03 8:31:51 PM
    pd1dts1dn00an1b9 |   8557.61 |0.00328457 |  28.1080690777 |    2018-11-03 8:31:51 PM
    pd1dts0m900an1b8 |   8519.41 |    0.7021 |    5981.477761 |    2018-11-03 8:19:21 PM
    pd1dts0cd00an1b7 |   8519.41 |    0.3582 |    3051.652662 |    2018-11-03 8:14:05 PM
    pd1dts08q00an1b6 |   8519.41 |    0.5668 |    4828.801588 |    2018-11-03 8:12:10 PM
    pd1dtrvrg00an1b5 |   8520.26 |    0.3417 |    2911.372842 |    2018-11-03 8:05:04 PM
    pd1dtrvir00an1b4 |   8520.26 |    0.6186 |    5270.632836 |    2018-11-03 8:00:27 PM
    pd1dtrv8900an1b3 |   8520.26 |    0.6496 |    5534.760896 |    2018-11-03 7:54:49 PM
    pd1dtruv600an1b2 |   8518.56 |    0.3569 |    3040.274064 |    2018-11-03 7:49:58 PM
    pd1dtrupa00an1b1 |   8520.26 |    0.6163 |    5251.036238 |    2018-11-03 7:46:50 PM
    pd1dtruhv00an1b0 |   8520.26 |    0.3933 |    3351.018258 |    2018-11-03 7:42:55 PM
    pd1dtru8500an1av |   8519.41 |    0.3911 |    3331.941251 |    2018-11-03 7:37:41 PM
    pd1dtrtuk00an1au |   8520.26 |    0.6211 |    5291.933486 |    2018-11-03 7:32:36 PM
    pd1dtrtma00an1at |   8521.12 |    0.4809 |    4097.806608 |    2018-11-03 7:28:10 PM
    pd1dtrt9j00an1as |   8518.56 |    0.7553 |    6434.068368 |    2018-11-03 7:21:23 PM
    pd1dtrt3l00an1ar |   8521.12 |    0.6935 |     5909.39672 |    2018-11-03 7:18:13 PM
    pd1dtrseu00an1aq |   8521.12 |    0.6986 |    5952.854432 |    2018-11-03 7:07:10 PM
    pd1dtrsb400an1ap |   8517.83 |    0.3926 |    3344.100058 |    2018-11-03 7:05:08 PM

Press any key to exit.

```

## Exception handling example
```csharp
try
{
    var result = client.GetOrderBookAsync("btczec", 20).GetAwaiter().GetResult();
}
catch (CoinFieldException ex)
{
    Console.WriteLine("Status: {0}, Message: {1}", ex.Status, ex.Message);

    foreach(var error in ex.Errors)
    {
        Console.WriteLine("Field: {0}, Location: {1} ", error.Field, error.Location);

        foreach(var message in error.Messages)
        {
            Console.WriteLine(message.ToString());
        }

        foreach (var type in error.Types)
        {
            Console.WriteLine(type.ToString());
        }
    }

    Console.WriteLine("Time: {0}", ex.Timestamp);
}
catch (Exception ex)
{
    Console.WriteLine("Error: {0}", ex.Message);
}
```

### Output
```
Status: 400, Message: Validation Error
Field: market, Location: params
"market" must be one of [btccad, ethcad, xrpcad, ltccad, btcusd, ethusd, xrpusd, ltcusd, btceur, etheur, xrpeur, ltceur, xrpjpy, xrpgbp, xrpaed, ethxrp, btcxrp, dashxrp, ltcxrp, zecxrp, btgxrp, bchxrp, zrxxrp, gntxrp, repxrp, omgxrp, saltxrp, batxrp, zilxrp, trxxrp]
any.allowOnly
Time: 2018-11-03 11:26:58 PM

```

## My related projects

* [QuadrigaCX.Api](https://github.com/RobJohnston/QuadrigaCX.Api)
* [Ndax.Api](https://github.com/RobJohnston/Ndax.Api) and [AlphaPoint.Api](https://github.com/RobJohnston/alphapoint.api/)
* [EzBtc.Api](https://github.com/RobJohnston/EzBtc.Api)
* [Coinsquare.Api](https://github.com/RobJohnston/Coinsquare.Api)
* [AnxPro.Api](https://github.com/RobJohnston/AnxPro.Api)

## Donation addresses
* Ripple (XRP): rK7D3QnTrYdkp1fGKKzHFNXZpqN8dUCfaf  Tag: 483
* BitCoin (BTC): 3HRvBkwFQ75WxhHmZjpj59iTL3ecqS4tF7
* Ethereum (ETH): 0xaed6becb8390c1616156be330ad09a75db4b47b4
* Stellar (XLM): GBVLC3JDKTEMS4I4LQ2RCNVSA2LBVO5FY6G3HPMEESWHB4S6PMC3IXPT  Memo: 130
* Litecoin (LTC): MECXCSdiiYAj1PeobCUGLYmpU7vm3UsyxR
* Bitcoin Cash (BCH): 3DMnrmTJAQZ6ztag1B3WLeE7uQYNr69rxA
* Dash (DASH): 7nq7YxodXuDKy8YwpZzzKD19gmo53yLJQC
* ZCash (ZEC): t3WMebzJcFjopK75tEoGXc2L94Dz7VeqEQF
* DigiByte (DGB): DBNYEyt3zjvbw5J1hbGe9KNhNNxPcrMbLv
* 0x (ZRX): 0xaed6becb8390c1616156be330ad09a75db4b47b4
