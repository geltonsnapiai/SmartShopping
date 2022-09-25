import http from 'k6/http';
import { check, group, sleep, fail } from 'k6';

export const options = {
    vus: 10, // 1 user looping for 1 minute
    duration: '1m',
    thresholds: {
        http_req_duration: ['p(99)<1500'], // 99% of requests must complete below 1.5s
    },
};

const BASE_URL = 'https://localhost:7100';
const EMAIL = 'test@test.com';
const PASSWORD = 'Test1234?';

export default () => {
    const loginPayload = JSON.stringify({
        email: EMAIL,
        password: PASSWORD,
    });

    const loginParams = {
        headers: {
            'Content-Type': 'application/json',
        },
    };

    const loginRes = http.post(`${BASE_URL}/api/auth/login`, loginPayload, loginParams);
    check(loginRes, {
        'logged in successfully': (resp) => resp.json('accessToken') !== '',
    });

    const userParams = {
        headers: {
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${loginRes.json('accessToken')}`,
        },
    };

    const userRes = http.get(`${BASE_URL}/api/auth/user`, userParams);
    check(userRes, { 'retrieve user details': (user) => user.json("email") === EMAIL });
};