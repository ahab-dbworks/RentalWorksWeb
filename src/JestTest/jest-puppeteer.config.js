module.exports = {
    launch: {
        dumpio: true,
        //headless: process.env.HEADLESS !== 'false',
        headless: false,
        args: [`--window-size=${1600},${1080}`]
    },
    browserContext: 'default'
};