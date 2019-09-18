const fs = require('fs');
const sgMail = require('@sendgrid/mail');
const { createLogger, format, transports } = require('winston');
const { combine, timestamp, label, printf } = format;
const myFormat: any = printf(({ level, message, label, timestamp }) => {
    return `${timestamp} ${level}: ${message}`;
});

export class Logging {
    //-----------------------------------------------------------------------------------------------------------------
    static logger = createLogger({
        format: combine(
            timestamp(),
            myFormat
        ),
        defaultMeta: {
            service: 'user-service'
        },
        transports: [
            new transports.Console(),
            new transports.File({ filename: 'error.log', level: 'error' }),
            new transports.File({ filename: 'combined.log', level: 'info' }),
        ]
    });
    //-----------------------------------------------------------------------------------------------------------------
    static async logInfo(msg: string): Promise<void> {
        Logging.logger.info(msg);
    }
    //-----------------------------------------------------------------------------------------------------------------
    static async logError(msg: string): Promise<void> {
        Logging.logger.error(msg);
    }
    //-----------------------------------------------------------------------------------------------------------------
    static async emailResults(): Promise<void> {
        let buffer = '';
        const combinedLog = fs.createReadStream("./combined.log", "utf8");
        combinedLog.on('data', chunk => {
            buffer += chunk;
        });
        combinedLog.on('end', () => {
            const result = buffer;
            console.log('result:', result);
            //  console.log('TYPEOF:', typeof result);
            //  result = result.toString();
            sgMail.setApiKey(process.env.SENDGRID_API_KEY);
            const msg = {
                to: 'joshpace@gmail.com',
                from: 'jpace@4wall.com',
                subject: 'SendGrid Sending logfile',
                text: 'result',
                html: '<strong>and easy to do anywhere, even with Node.js</strong>',
                attachments: [
                    {
                        content: 'Some base 64 encoded attachment content',
                        filename: 'some-attachment.txt',
                        type: 'plain/text',
                        disposition: 'attachment',
                        contentId: 'mytext'
                    },
                ],
            };
            sgMail.send(msg)
                .then((res) => console.log('RES: ', res))
                .catch((err) => console.log('ERR: ', err))
        });
    }
    //---------------------------------------------------------------------------------------
}