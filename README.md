# XLSTimedMessages
Skater XL Multiplayer Server Plugin for Sending Messages on an Interval

# Installing

Download the newest version from the Releases tab. You should have an `XLSTimedMessages.zip` file. Inside of this zip archive you'll find a folder called `Macs.XLSTimedMessages`, drag this folder into your `Plugins` directory of your `0.9.0`+ XLMultiplayer Server directory. 

This is all you need to do to install the plugin.

# Configuration

Inside of the `Macs.XLSTimedMessages` folder you will find a `Config.json` file. This file holds all the settings for the plugin. You can set your messages, intervals, durations, and message color. Below is an example configuration:

```
{
  "Interval": 5, // Interval for messages to be sent
  "IntervalType": "minutes", // Type of interval. Takes: milliseconds, seconds, minutes
  "Duration": 10, // Duration of message to be displayed
  "DurationType": "seconds", // Type of Duration. Takes: milliseconds, seconds, minutes
  "MsgColor":  "#ff0000", // Message Color. Takes 3-bit or 6-bit hex values.
  "Messages": [
    "This is the first message",
    "This is the second message"
  ], // Message array. Add an element for each message. By default it will scroll through from 0, 1, 2.... and so forth
  "RandomizeMessages": false // Randomize the messages. Doesn't guarantee true randomness
}
```

You'll need to restart your server each time you make changes to the configuration.
