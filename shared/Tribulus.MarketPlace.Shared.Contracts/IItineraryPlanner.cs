﻿
using MassTransit;
using MassTransit.Courier;
using System;

namespace Tribulus.MarketPlace;

public interface IItineraryPlanner<in T>
{
    void ProduceItinerary(T value, ItineraryBuilder builder);
}
