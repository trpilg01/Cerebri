
interface ErrorPopUpProps {
    setShowError: (value: boolean) => void;
    message: string | null;
}

const ErrorPopUp: React.FC<ErrorPopUpProps> = ({ message, setShowError }) => {

    return (
        <div 
            className="fixed flex flex-col z-[1000] h-[300px] w-[400px] shadow-2xl p-4 rounded-lg bg-slate-200
            justify-between items-center"
        >
            <h1 className="text-2xl font-bold">Error!</h1>
            <a className="h-24 font-bold">{message}</a>
            <button
                className="bg-blizzardBlue p-1 rounded-md shadow-sm font-bold" 
                onClick={() => setShowError(false)}>Okay</button>
        </div>
    )
}

export default ErrorPopUp;