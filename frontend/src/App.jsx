import WorkflowBuilder from "./components/WorkflowBuilder";

export default function App() {
  return (
    <>
      <div class="flex flex-col h-full">
        <div class="basis-2/12 m-2 font-bold" >
          <h1 class="text-5xl table-header-group">LetMeKnow:</h1>
        </div>

        <WorkflowBuilder />
      </div>
    </>
  );
}
