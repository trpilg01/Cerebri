import { Journal } from "data/dataTypes";
import { useEffect, useState } from "react";
import { requestDeleteJournal, requestJournals } from "services"


const useJournals = () => {
    const [journals, setJournals] = useState<Journal[] | undefined>(undefined);
    const [error, setError] = useState<string | null>(null);

    const fetchJournals = async () => {
        try {
            const journals = await requestJournals();
            setJournals(journals)
        } catch (err: any) {
            setError(err.message ?? "Error fetching journals");
        }
    };

    const deleteJournals = async(id: string) => {
        try {
            await requestDeleteJournal(id);
        } catch (err: any) {
            setError(err.message ?? "Error deleting journal");
        }
    };

    useEffect(() => {
        fetchJournals();
    }, []);

    return { journals, fetchJournals, deleteJournals, error, setError}
};

export default useJournals;