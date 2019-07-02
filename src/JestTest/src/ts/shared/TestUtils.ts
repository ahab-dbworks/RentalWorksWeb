export class TestUtils {
    static async sleepAsync(timeout: number): Promise<void> {
        return new Promise<void>((resolve, reject) => {
            try {
                setTimeout(() => {
                    resolve();
                }, timeout);    
            } catch(ex) {
                reject(ex);
            }
        });
    }
}