﻿using FirebaseAdmin.Messaging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mealmate.Api.Application.Firebase
{
    public class FCMService : IFCMService
    {
        private FirebaseMessaging _fCMDefaultInstance;
        public FCMService()
        {
            _fCMDefaultInstance = FirebaseMessaging.DefaultInstance;
        }
        public async Task<string> SendToTokenAsync(string registrationToken, Notification notification)
        {
            var message = new Message()
            {
                Notification = notification,
                Token = registrationToken
            };

            return await _fCMDefaultInstance.SendAsync(message);
        }

        public async Task SendToTopicAsync()
        {
            // [START send_to_topic]
            // The topic name can be optionally prefixed with "/topics/".
            var topic = "highScores";

            // See documentation on defining a message payload.
            var message = new Message()
            {
                Data = new Dictionary<string, string>()
                {
                    { "score", "850" },
                    { "time", "2:45" },
                },
                Topic = topic,
            };

            // Send a message to the devices subscribed to the provided topic.
            string response = await _fCMDefaultInstance.SendAsync(message);
            // Response is a message ID string.
            Console.WriteLine("Successfully sent message: " + response);
            // [END send_to_topic]
        }

        public async Task SendToConditionAsync()
        {
            // [START send_to_condition]
            // Define a condition which will send to devices which are subscribed
            // to either the Google stock or the tech industry topics.
            var condition = "'stock-GOOG' in topics || 'industry-tech' in topics";

            // See documentation on defining a message payload.
            var message = new Message()
            {
                Notification = new Notification()
                {
                    Title = "$GOOG up 1.43% on the day",
                    Body = "$GOOG gained 11.80 points to close at 835.67, up 1.43% on the day.",
                },
                Condition = condition,
            };

            // Send a message to devices subscribed to the combination of topics
            // specified by the provided condition.
            string response = await _fCMDefaultInstance.SendAsync(message);
            // Response is a message ID string.
            Console.WriteLine("Successfully sent message: " + response);
            // [END send_to_condition]
        }

        public async Task SendDryRunAsync()
        {
            var message = new Message()
            {
                Data = new Dictionary<string, string>()
                {
                    { "score", "850" },
                    { "time", "2:45" },
                },
                Token = "token",
            };

            // [START send_dry_run]
            // Send a message in the dry run mode.
            string response = await _fCMDefaultInstance.SendAsync(
                message, dryRun: true);
            // Response is a message ID string.
            Console.WriteLine("Dry run successful: " + response);
            // [END send_dry_run]
        }

        public async Task SendAllAsync()
        {
            var registrationToken = "YOUR_REGISTRATION_TOKEN";
            // [START send_all]
            // Create a list containing up to 100 messages.
            var messages = new List<Message>()
            {
                new Message()
                {
                    Notification = new Notification()
                    {
                        Title = "Price drop",
                        Body = "5% off all electronics",
                    },
                    Token = registrationToken,
                },
                new Message()
                {
                    Notification = new Notification()
                    {
                        Title = "Price drop",
                        Body = "2% off all books",
                    },
                    Topic = "readers-club",
                },
            };

            var response = await _fCMDefaultInstance.SendAllAsync(messages);
            // See the BatchResponse reference documentation
            // for the contents of response.
            Console.WriteLine($"{response.SuccessCount} messages were sent successfully");
            // [END send_all]
        }

        public async Task<BatchResponse> SendMulticastAsync(List<string> registrationTokens,Notification notification)
        {
        
            var message = new MulticastMessage()
            {
                Tokens = registrationTokens,
                Notification = notification
            };

            return await _fCMDefaultInstance.SendMulticastAsync(message);
        }

        public async Task SendMulticastAndHandleErrorsAsync()
        {
            // [START send_multicast_error]
            // These registration tokens come from the client FCM SDKs.
            var registrationTokens = new List<string>()
            {
                "YOUR_REGISTRATION_TOKEN_1",
                // ...
                "YOUR_REGISTRATION_TOKEN_n",
            };
            var message = new MulticastMessage()
            {
                Tokens = registrationTokens,
                Data = new Dictionary<string, string>()
                {
                    { "score", "850" },
                    { "time", "2:45" },
                },
            };

            var response = await _fCMDefaultInstance.SendMulticastAsync(message);
            if (response.FailureCount > 0)
            {
                var failedTokens = new List<string>();
                for (var i = 0; i < response.Responses.Count; i++)
                {
                    if (!response.Responses[i].IsSuccess)
                    {
                        // The order of responses corresponds to the order of the registration tokens.
                        failedTokens.Add(registrationTokens[i]);
                    }
                }

                Console.WriteLine($"List of tokens that caused failures: {failedTokens}");
            }

            // [END send_multicast_error]
        }

        public Message CreateAndroidMessage()
        {
            // [START android_message]
            var message = new Message
            {
                Android = new AndroidConfig()
                {
                    TimeToLive = TimeSpan.FromHours(1),
                    Priority = Priority.Normal,
                    Notification = new AndroidNotification()
                    {
                        Title = "$GOOG up 1.43% on the day",
                        Body = "$GOOG gained 11.80 points to close at 835.67, up 1.43% on the day.",
                        Icon = "stock_ticker_update",
                        Color = "#f45342",
                    },
                },
                Topic = "industry-tech",
            };
            // [END android_message]
            return message;
        }

        public Message CreateAPNSMessage()
        {
            // [START apns_message]
            var message = new Message
            {
                Apns = new ApnsConfig()
                {
                    Headers = new Dictionary<string, string>()
                    {
                        { "apns-priority", "10" },
                    },
                    Aps = new Aps()
                    {
                        Alert = new ApsAlert()
                        {
                            Title = "$GOOG up 1.43% on the day",
                            Body = "$GOOG gained 11.80 points to close at 835.67, up 1.43% "
                                + "on the day.",
                        },
                        Badge = 42,
                    },
                },
                Topic = "industry-tech",
            };
            // [END apns_message]
            return message;
        }

        public Message CreateWebpushMessage()
        {
            // [START webpush_message]
            var message = new Message
            {
                Webpush = new WebpushConfig()
                {
                    Notification = new WebpushNotification()
                    {
                        Title = "$GOOG up 1.43% on the day",
                        Body = "$GOOG gained 11.80 points to close at 835.67, up 1.43% on the day.",
                        Icon = "https://my-server/icon.png",
                    },
                },
                Topic = "industry-tech",
            };
            // [END webpush_message]
            return message;
        }

        public Message CreateMultiPlatformsMessage()
        {
            // [START multi_platforms_message]
            var message = new Message
            {
                Notification = new Notification()
                {
                    Title = "$GOOG up 1.43% on the day",
                    Body = "$GOOG gained 11.80 points to close at 835.67, up 1.43% on the day.",
                },
                Android = new AndroidConfig()
                {
                    TimeToLive = TimeSpan.FromHours(1),
                    Notification = new AndroidNotification()
                    {
                        Icon = "stock_ticker_update",
                        Color = "#f45342",
                    },
                },
                Apns = new ApnsConfig()
                {
                    Aps = new Aps()
                    {
                        Badge = 42,
                    },
                },
                Topic = "industry-tech",
            };
            // [END multi_platforms_message]
            return message;
        }

        public async Task SubscribeToTopicAsync(string topic)
        {
            // [START subscribe_to_topic]
            // These registration tokens come from the client FCM SDKs.
            var registrationTokens = new List<string>()
            {
                "YOUR_REGISTRATION_TOKEN_1",
                // ...
                "YOUR_REGISTRATION_TOKEN_n",
            };

            // Subscribe the devices corresponding to the registration tokens to the
            // topic
            var response = await _fCMDefaultInstance.SubscribeToTopicAsync(
                registrationTokens, topic);
            // See the TopicManagementResponse reference documentation
            // for the contents of response.
            Console.WriteLine($"{response.SuccessCount} tokens were subscribed successfully");
            // [END subscribe_to_topic]
        }

        public async Task UnsubscribeFromTopicAsync(string topic)
        {
            // [START unsubscribe_from_topic]
            // These registration tokens come from the client FCM SDKs.
            var registrationTokens = new List<string>()
            {
                "YOUR_REGISTRATION_TOKEN_1",
                // ...
                "YOUR_REGISTRATION_TOKEN_n",
            };

            // Unsubscribe the devices corresponding to the registration tokens from the
            // topic
            var response = await _fCMDefaultInstance.UnsubscribeFromTopicAsync(
                registrationTokens, topic);
            // See the TopicManagementResponse reference documentation
            // for the contents of response.
            Console.WriteLine($"{response.SuccessCount} tokens were unsubscribed successfully");
            // [END unsubscribe_from_topic]
        }
    }
}
