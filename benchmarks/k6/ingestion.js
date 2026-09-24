import http from 'k6/http';
import { check } from 'k6';

export const options = {

    scenarios:{
        event_ingestion:{
            duration: '30s',
            maxVUs: 10
        }
    }
}

export default function (){
    const payload = JSON.stringify({
        Type:'OrderCreated',
        Payload:{
            orderId:`ORD-${__VU}-${__ITER}`,
            amount:2480,
            currency:'INR'
        }
    });

    const response = http.post(
        'http://host.docker.internal:5000/api/events',
        payload,
        {
            headers:{
                'Content-Type':'application/json',
            }
        }
    );

    check(response,{
        'event accepted':(r) => r.status === 200,
    })
}