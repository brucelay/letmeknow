import { createSignal } from "solid-js";
import WorkflowBuilder from "./components/WorkflowBuilder";

export default function App() {
  const [number, setNumber] = createSignal("")

  return (
    <>
      <div class="flex flex-col h-full">
        <div class="basis-2/12 p-5 ml-2 mt-2 font-bold shadow" >
          <h1 id="title" class="text-7xl table-header-group">LetMe<span class="text-orange-600">Know</span>,</h1>
          <div class="mt-5">
            <input onInput={(e) => setNumber(e.target.value)} class="text-5xl rounded" type="text" placeholder="+4407192874981"></input>
          </div>
        </div>
        <WorkflowBuilder number={number}/>
      </div>
    </>
  );
}
