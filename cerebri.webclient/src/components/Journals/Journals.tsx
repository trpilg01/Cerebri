import { Journal } from "data/dataTypes";
import { useState } from "react";
import { CreateJournal, ViewJournal, ErrorPopUp } from '..';
import JournalsTable from "components/Journals/JournalsTable";
import useJournals from "hooks/useJournals";

const Journals = () => {
    const { journals, deleteJournals, error, setError} = useJournals();
    const [showCreateJournal, setShowCreateJournal] = useState<boolean>(false);
    const [selectedJournal, setSelectedJournal] = useState<Journal | null>(null);

    return(
        <div className="flex flex-col h-full w-full justify-center items-center bg-pearlLusta">
            <h1 className="text-2xl font-bold">Journals</h1>
            <JournalsTable 
                journals={journals}
                onDelete={deleteJournals}
                onCreate={setShowCreateJournal}
                onView={setSelectedJournal}
            />
            {selectedJournal && <ViewJournal journal={selectedJournal} setSelectedJournal={setSelectedJournal}/>}
            {showCreateJournal && <CreateJournal setShowOverlay={setShowCreateJournal}/>}
            {error && <ErrorPopUp message={error} setError={setError} />}
        </div>
    );
};

export default Journals;