//
//  BitskiUnitySDK.m
//  BitskiUnitySDK
//
//  Created by Patrick Tescher on 6/25/18.
//  Copyright © 2018 Out There Labs. All rights reserved.
//

#import "BitskiUnitySDK.h"

NSMutableDictionary<NSUUID*, id<OIDAuthorizationFlowSession>>* AuthFlows() {
    static dispatch_once_t once;
    static id sharedInstance;
    dispatch_once(&once, ^{
        sharedInstance = [NSMutableDictionary new];
    });
    return sharedInstance;
}

extern "C" {
    void BitskiSignIn(char *clientID, char *redirectUri, BitskiSignInCallback callback) {

        NSURL *authorizationEndpoint = [NSURL URLWithString:@"https://account.bitski.com/oauth2/auth"];
        NSURL *tokenEndpoint = [NSURL URLWithString:@"https://account.bitski.com/oauth2/token"];

        OIDServiceConfiguration *configuration = [[OIDServiceConfiguration alloc] initWithAuthorizationEndpoint:authorizationEndpoint tokenEndpoint:tokenEndpoint];

        OIDAuthorizationRequest *request = [[OIDAuthorizationRequest alloc] initWithConfiguration:configuration
                                                                                         clientId:[NSString stringWithUTF8String: clientID]
                                                                                           scopes:@[OIDScopeOpenID, @"offline"]
                                                                                      redirectURL:[NSURL URLWithString:[NSString stringWithUTF8String: redirectUri]]
                                                                                     responseType:OIDResponseTypeCode
                                                                             additionalParameters:nil];

        NSUUID *authSessionID = [NSUUID new];

        id<OIDAuthorizationFlowSession> authFlow = [OIDAuthState authStateByPresentingAuthorizationRequest:request
                                                                                  presentingViewController:[UIApplication sharedApplication].keyWindow.rootViewController
                                                                                                  callback:^(OIDAuthState *_Nullable authState,
                                                                                                             NSError *_Nullable error) {
                                                                                                      if (authState) {
                                                                                                          const char *__nullable tokenString = authState.lastTokenResponse.accessToken.UTF8String;
                                                                                                          if (callback) {
                                                                                                              callback(tokenString, nil);
                                                                                                          }
                                                                                                      }

                                                                                                      if (error) {
                                                                                                          const char *__nullable errorString = error.localizedDescription.UTF8String;
                                                                                                          if (callback) {
                                                                                                              callback(nil, errorString);
                                                                                                          }
                                                                                                      }
                                                                                                  }];

        [AuthFlows() setObject:authFlow forKey:authSessionID];
    }
}
