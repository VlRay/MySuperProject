db = db.getSiblingDB('projectsdb');

db.projects.insertMany([
    {
        userId: 3,
        name: "my super project 1",
        charts: [
            { symbol: "EURUSD", timeframe: "M5", indicators: [] },
            {
                symbol: "USDJPY", timeframe: "H1",
                indicators: [
                    { name: "BB", parameters: "a=1;b=2;c=3" },
                    { name: "MA", parameters: "a=1;b=2;c=3" }
                ]
            }
        ]
    },
    {
        userId: 3,
        name: "my super project 2",
        charts: [
            {
                symbol: "EURUSD", timeframe: "M5",
                indicators: [{ name: "MA", parameters: "a=1;b=2;c=3" }]
            }
        ]
    },
    {
        userId: 3,
        name: "my super project 3",
        charts: []
    },
    {
        userId: 2,
        name: "project 1",
        charts: [
            {
                symbol: "EURUSD", timeframe: "H1",
                indicators: [{ name: "RSI", parameters: "a=1;b=2;c=3" }]
            }
        ]
    },
    {
        userId: 2,
        name: "project 2",
        charts: [
            {
                symbol: "USDJPY", timeframe: "H1",
                indicators: [{ name: "MA", parameters: "a=1;b=2;c=3" }]
            }
        ]
    },
    {
        userId: 1,
        name: "project 3",
        charts: [
            {
                symbol: "EURUSD", timeframe: "M5",
                indicators: [
                    { name: "RSI", parameters: "a=1;b=2;c=3" },
                    { name: "MA", parameters: "a=1;b=2;c=3" }
                ]
            }
        ]
    }
]);
