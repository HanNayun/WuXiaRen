using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Core;
using GamePlay.GameDate;

public class TestGameDate
{
    // A Test behaves as an ordinary method
    [Test]
    public async void TestGameDateSimplePasses()
    {
        var dateObject = AddressableAssetLoader.LoadAssetAsync<GameDateData>(GameDateData.GameDateObjectAddress);
        
    }
}