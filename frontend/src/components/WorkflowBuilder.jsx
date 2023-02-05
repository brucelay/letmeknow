import ComponentEdit from "./ComponentEdit";
import ComponentSelect from "./ComponentSelect";
import { createStore } from "solid-js/store";
import { createSignal } from "solid-js";
import { modules as modulesObject } from "./modules/modules";
import { unwrap } from "solid-js/store";

export default function WorkflowBuilder() {
  const [functions, setFunctions] = createStore([]);

  const [modules, setModules] = createSignal(modulesObject);

  function printWorkflow() {
    const unwrappedFunctions = unwrap(functions);
    console.log(JSON.stringify(unwrappedFunctions, null, 2));
  }

  async function runWorkflow() {
    const req = await fetch(
      "https://letmeknow-api.onrender.com/Workflow/CreateWorkflow",
      {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(functions, null, 2),
      }
    );

    console.log(req.status);
  }

  return (
    <>
      <div class="basis-8/12 p-5 bg-slate-200 shadow-inner shadow-inner-md">
        <div class="flex flex-row">
          <div class="basis-1/4 font-bold">
            <ComponentSelect
              modules={modules}
              setModules={setModules}
              functions={functions}
              setFunctions={setFunctions}
            />
          </div>
          <div class="basis-3/4">
            <ComponentEdit functions={functions} setFunctions={setFunctions} />
          </div>
        </div>
      </div>
      <div class="basis-2/12">
        <button onClick={runWorkflow}>Run Workflow</button>
      </div>
    </>
  );
}
